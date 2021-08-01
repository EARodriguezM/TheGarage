using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using TheGarageAPI.Helpers;
using TheGarageAPI.Entities;
using TheGarageAPI.Models.Vehicle;

namespace TheGarageAPI.Servicies
{

    public interface IVehicleService
    {
        Task<Vehicle> Register(RegisterRequest registerRequest);
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetByPlate(string vehiclePlate);
        Task Update(UpdateRequest updateRequest);
        Task Delete(string vehiclePlate);
    }
    public class VehicleService : IVehicleService
    {

        private readonly TheGarageContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettigns _appSettings;

        public VehicleService(TheGarageContext context, IMapper mapper, IOptions<AppSettigns> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        #region Public methods

        public async Task<Vehicle> Register(RegisterRequest registerRequest) 
        {
            var vehicle = _mapper.Map<Vehicle>(registerRequest);

            if (await GetByPlate(vehicle.VehiclePlate) == null)
                throw new AppException("Vehicle plate \"" + vehicle.VehiclePlate + "\" is registered");

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return vehicles;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<Vehicle> GetByPlate(string vehiclePlate)
        {
            var vehicleFinded = await _context.Vehicles.FindAsync(vehiclePlate);
            return vehicleFinded;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {

            var vehicle = _mapper.Map<Vehicle>(updateRequest);

            var vehicleInDatabase = await GetByPlate(updateRequest.VehiclePlate);

            if (vehicleInDatabase == null)
                throw new AppException("Vehicle not found");

            //Update city of vehicule plate if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.PlateCity) && vehicle.PlateCity != vehicleInDatabase.PlateCity)
            {
                vehicleInDatabase.PlateCity = vehicle.PlateCity;
            }

            //Update vehicle type if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.VehicleTypeId.ToString()) && vehicle.VehicleTypeId != vehicleInDatabase.VehicleTypeId)
            {
                vehicleInDatabase.VehicleTypeId = vehicle.VehicleTypeId;
            }

            //Update brand if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.Brand) && vehicle.Brand != vehicleInDatabase.Brand)
            {
                vehicleInDatabase.Brand = vehicle.Brand;
            }

            //Update city of vehicule plate if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.PlateCity) && vehicle.PlateCity != vehicleInDatabase.PlateCity)
            {
                vehicleInDatabase.PlateCity = vehicle.PlateCity;
            }

            //Update line if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.Line) && vehicle.Line != vehicleInDatabase.Line)
            {
                vehicleInDatabase.Line = vehicle.Line;
            }

            //Update model if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.Model) && vehicle.Model != vehicleInDatabase.Model)
            {
                vehicleInDatabase.Model = vehicle.Model;
            }

            //Update color if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.Color) && vehicle.Color != vehicleInDatabase.Color)
            {
                vehicleInDatabase.Color = vehicle.Color;
            }

            //Update description if it has changed
            if (!string.IsNullOrWhiteSpace(vehicleInDatabase.Description) && vehicle.Description != vehicleInDatabase.Description)
            {
                vehicleInDatabase.Description = vehicle.Description;
            }
            
            _context.Vehicles.Update(vehicleInDatabase);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete(string vehiclePlate)
        {
            var vehicle = await GetByPlate(vehiclePlate);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}