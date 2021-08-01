using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using AutoMapper;

using TheGarageAPI.Helpers;
using TheGarageAPI.Entities;
using TheGarageAPI.Models.DataUser;

namespace TheGarageAPI.Servicies
{

    public interface IDataUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
        Task<DataUser> Register(RegisterRequest registerRequest);
        Task<IEnumerable<DataUser>> GetAll();
        Task<DataUser> GetById(string dataUserId);
        Task Update(UpdateRequest updateRequest);

        Task Delete (string dataUserId);
    }

    public class DataUserService : IDataUserService
    {
        //Do global database context, mapper from automapper and my appsettings for only read
        private readonly TheGarageContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettigns _appSettings;

        public DataUserService(TheGarageContext context, IMapper mapper, IOptions<AppSettigns> appSettings)
        {
            //Assign arguments to global readonly variables
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        #region Public methods

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest)
        {
            //Search user in database with email filter
            var user = await _context.DataUsers.SingleOrDefaultAsync(x => x.Email == authenticateRequest.Email);

            if (user == null)
                return null;
            
            if (!VerifyPasswordHash(authenticateRequest.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            //Delete passwords from user response and generate token to return
            var authenticateResponse = new AuthenticateResponse(user.WithoutPassword(), GenerateToken(user));

            return authenticateResponse;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<DataUser> Register(RegisterRequest registerRequest)
        {
            registerRequest.Email = registerRequest.Email.ToLower();

            var dataUser = _mapper.Map<DataUser>(registerRequest);
            var password = registerRequest.Password;

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (await _context.DataUsers.AnyAsync(x => x.Email == dataUser.Email))
                throw new AppException("Email \"" + dataUser.Email + "\" was taken");

            if (await GetById(dataUser.DataUserId) != null)
                throw new AppException("Id \"" + dataUser.DataUserId + "\" is registered");


            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            dataUser.PasswordHash = passwordHash;
            dataUser.PasswordSalt = passwordSalt;

            await _context.DataUsers.AddAsync(dataUser);
            await _context.SaveChangesAsync();

            return dataUser.WithoutPassword();
        }
        public async Task<IEnumerable<DataUser>> GetAll()
        {
            var dataUsers =  await _context.DataUsers.ToListAsync();
            return dataUsers.WithoutPasswords();
        }
        ////////////////////////////////////////////////////////////////////////////////
        public async Task<DataUser> GetById(string dataUserId)
        {
            var userFinded = await _context.DataUsers.FindAsync(dataUserId);
            return userFinded.WithoutPassword();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {

            var dataUser = _mapper.Map<DataUser>(updateRequest);
            
            var user = await GetById(updateRequest.DataUserId);

            if (user == null)
                throw new AppException("User not found");

            //Update email if it has changed
            if (!string.IsNullOrWhiteSpace(dataUser.Email) && dataUser.Email != user.Email)
            {
                //Throw error if the new email is already taken
                if (_context.DataUsers.Any(x => x.Email == dataUser.Email))
                    throw new AppException("Username " + dataUser.Email + " is already taken");

                user.Email = dataUser.Email;
            }

            //Update phone if it has changed
            if (!string.IsNullOrWhiteSpace(dataUser.Mobile) && dataUser.Mobile != user.Mobile)
            {
                //Throw error if the new phone is already taken
                if (_context.DataUsers.Any(x => x.Mobile == dataUser.Mobile))
                    throw new AppException("Username " + dataUser.Mobile + " is already taken");

                user.Mobile = dataUser.Mobile;
            }

            if (!string.IsNullOrWhiteSpace(dataUser.UserTypeId.ToString()) && dataUser.UserTypeId != user.UserTypeId)
            {
                user.UserTypeId = dataUser.UserTypeId;
            }

            //Update user properties if provided
            if (!string.IsNullOrWhiteSpace(dataUser.FirstName))
                user.FirstName = dataUser.FirstName;
            if (!string.IsNullOrWhiteSpace(dataUser.SecondName))
                user.SecondName = dataUser.SecondName;
            if (!string.IsNullOrWhiteSpace(dataUser.FirstSurname))
                user.FirstSurname = dataUser.FirstSurname;
            if (!string.IsNullOrWhiteSpace(dataUser.SecondSurname))
                user.SecondSurname = dataUser.SecondSurname;

            //Update password if provided
            if (!string.IsNullOrWhiteSpace(updateRequest.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(updateRequest.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.DataUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete (string dataUserId)
        {
            var user = await _context.DataUsers.FindAsync(dataUserId);
            if (user != null)
            {
                _context.DataUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        #endregion


        #region Private helper methods

        //Generate token to communication client-server usings claim with user id and type
        private string GenerateToken(DataUser dataUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, dataUser.DataUserId.ToString()),
                    new Claim(ClaimTypes.Role, dataUser.UserTypeId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
 
        //Create password hash and salt from password using HMACSHA512
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Verify password in authentication using the stored hash and salt
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        #endregion
        
        
        
    }
}