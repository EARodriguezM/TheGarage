using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;


using AutoMapper;

using TheGarageAPI.Helpers;
using TheGarageAPI.Entities;
using TheGarageAPI.Models.VehicleType;

namespace TheGarageAPI.Servicies
{

    public interface IVehicleTypeService
    {
        Task<VehicleType> Register(RegisterRequest registerRequest);
        Task<IEnumerable<VehicleType>> GetAll();
        Task<VehicleType> GetVehicleTypeById(string vehicleTypeId);
        Task Update(UpdateRequest updateRequest);
        Task Delete(string vehicleTypeId);
    }
    public class VehicleTypeService : IVehicleTypeService
    {

        private readonly TheGarageContext _context;
        private readonly IMapper _mapper;

        public VehicleTypeService(TheGarageContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VehicleType> Register(RegisterRequest registerRequest)
        {
            registerRequest.Description = registerRequest.Description.ToUpper();

            var vehicleType = _mapper.Map<VehicleType>(registerRequest);

            if(await _context.VehicleTypes.AnyAsync(x => x.Description == vehicleType.Description))
                throw new AppException("The vehicle type \"" + vehicleType.Description + "\" are already registered");

            await _context.VehicleTypes.AddAsync(vehicleType);
            await _context.SaveChangesAsync();

            return vehicleType;
                
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<IEnumerable<VehicleType>> GetAll()
        {
            var vehicleTypes = await _context.VehicleTypes.ToListAsync();
            return vehicleTypes;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<VehicleType> GetVehicleTypeById(string vehicleTypeId)
        {
            var vehicleTypesFinded = await _context.VehicleTypes.FindAsync(vehicleTypeId);
            return vehicleTypesFinded;
        }
        
        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {
            var vehicleType = _mapper.Map<VehicleType>(updateRequest);

            var vehicleTypeInDatabase = await GetVehicleTypeById(updateRequest.VehicleTypeId.ToString());

            if (vehicleTypeInDatabase == null)
                throw new AppException("Vehicle type not found");

            //Update description of vehicle type if iot has changed
            if (!string.IsNullOrWhiteSpace(vehicleTypeInDatabase.Description) && vehicleType.Description != vehicleTypeInDatabase.Description)
            {
                vehicleTypeInDatabase.Description = vehicleType.Description;
            }

            _context.VehicleTypes.Update(vehicleTypeInDatabase);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete(string vehicleTypeId)
        {
            var vehicleType = await GetVehicleTypeById(vehicleTypeId);
            if (vehicleType != null)
            {
                _context.VehicleTypes.Remove(vehicleType);
                await _context.SaveChangesAsync();
            }
        }
    }
}