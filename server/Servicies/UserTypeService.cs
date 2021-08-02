using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using TheGarageAPI.Helpers;
using TheGarageAPI.Entities;
using TheGarageAPI.Models.UserType;

namespace TheGarageAPI.Servicies
{
    public interface IUserTypeService
    {
        Task<UserType> Register(RegisterRequest registerRequest);
        Task<IEnumerable<UserType>> GetAll();
        Task<UserType> GetUserTypeById(string userTypeId);
        Task Update(UpdateRequest updateRequest);
        Task Delete(string userTypeId);
    }
    public class UserTypeService : IUserTypeService
    {
        private readonly TheGarageContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettigns _appSettings;

        public UserTypeService(TheGarageContext context, IMapper mapper, AppSettigns appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<UserType> Register(RegisterRequest registerRequest)
        {
            registerRequest.Description = registerRequest.Description.ToUpper();

            var userType = _mapper.Map<UserType>(registerRequest);

            if(await _context.UserTypes.AnyAsync(x => x.Description == userType.Description))
                throw new AppException("The user type \"" + userType.Description + "\" is already registered");

            await _context.UserTypes.AddAsync(userType);
            await _context.SaveChangesAsync();

            return userType;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<IEnumerable<UserType>> GetAll()
        {
            var userTypes = await _context.UserTypes.ToListAsync();
            return userTypes;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<UserType> GetUserTypeById(string userTypeId)
        {
            var userTypeFinded = await _context.UserTypes.FindAsync(userTypeId);
            return userTypeFinded;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {
            var userType = _mapper.Map<UserType>(updateRequest);

            var userTypeInDatabase = await GetUserTypeById(updateRequest.UserTypeId.ToString());

            if (userTypeInDatabase == null)
                throw new AppException("User type not found");

            //Update description of slot status if it has changed
            if (!string.IsNullOrWhiteSpace(userTypeInDatabase.Description) && userType.Description != userTypeInDatabase.Description)
            {
                userTypeInDatabase.Description = userType.Description;
            }

            //Update status of slot status if it has changed
            if (!string.IsNullOrWhiteSpace(userTypeInDatabase.Status.ToString()) && userType.Status != userTypeInDatabase.Status)
            {
                userTypeInDatabase.Status = userType.Status;
            }

            _context.UserTypes.Update(userTypeInDatabase);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete(string userTypeId)
        {
            var userType = await GetUserTypeById(userTypeId);
            if (userType != null)
            {
                _context.UserTypes.Remove(userType);
                await _context.SaveChangesAsync();
            }
        }
    }
}