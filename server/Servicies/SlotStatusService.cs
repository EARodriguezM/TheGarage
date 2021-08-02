using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using TheGarageAPI.Helpers;
using TheGarageAPI.Entities;
using TheGarageAPI.Models.SlotStatus;

namespace TheGarageAPI.Servicies
{

    public interface ISlotStatusService
    {
        Task<SlotStatus> Register(RegisterRequest registerRequest);
        Task<IEnumerable<SlotStatus>> GetAll();
        Task<SlotStatus> GetSlotStatusById(string slotStatusId);
        Task Update(UpdateRequest updateRequest);
        Task Delete(string slotStatusId);
    }

    public class SlotStatusService : ISlotStatusService
    {

        private readonly TheGarageContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettigns _appSettings;

        public SlotStatusService(TheGarageContext context, IMapper mapper, AppSettigns appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<SlotStatus> Register(RegisterRequest registerRequest)
        {
            registerRequest.Description = registerRequest.Description.ToUpper();

            var slotStatus = _mapper.Map<SlotStatus>(registerRequest);

            if(await _context.SlotStatuses.AnyAsync(x => x.Description == slotStatus.Description))
                throw new AppException("The slot status \"" + slotStatus.Description + "\" are already registered");

            await _context.SlotStatuses.AddAsync(slotStatus);
            await _context.SaveChangesAsync();

            return slotStatus;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<IEnumerable<SlotStatus>> GetAll()
        {
            var slotStatuses = await _context.SlotStatuses.ToListAsync();
            return slotStatuses;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<SlotStatus> GetSlotStatusById(string slotStatusId)
        {
            var sloStatusesFinded = await _context.SlotStatuses.FindAsync(slotStatusId);
            return sloStatusesFinded;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {
            var slotStatus = _mapper.Map<SlotStatus>(updateRequest);

            var slotStatusInDatabase = await GetSlotStatusById(updateRequest.SlotStatusId.ToString());

            if (slotStatusInDatabase == null)
                throw new AppException("Vehicle type not found");

            //Update description of slot status if it has changed
            if (!string.IsNullOrWhiteSpace(slotStatusInDatabase.Description) && slotStatus.Description != slotStatusInDatabase.Description)
            {
                slotStatusInDatabase.Description = slotStatus.Description;
            }

            //Update status of slot status if it has changed
            if (!string.IsNullOrWhiteSpace(slotStatusInDatabase.Status.ToString()) && slotStatus.Status != slotStatusInDatabase.Status)
            {
                slotStatusInDatabase.Status = slotStatus.Status;
            }

            _context.SlotStatuses.Update(slotStatusInDatabase);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete(string slotStatusId)
        {
            var slotStatus = await GetSlotStatusById(slotStatusId);
            if (slotStatus != null)
            {
                _context.SlotStatuses.Remove(slotStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}