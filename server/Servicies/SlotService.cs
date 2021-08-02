using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using TheGarageAPI.Helpers;
using TheGarageAPI.Entities;
using TheGarageAPI.Models.Slot;

namespace TheGarageAPI.Servicies
{

    public interface ISlotService
    {
        Task<Slot> Register(RegisterRequest registerRequest);
        Task<IEnumerable<Slot>> GetAll();
        Task<Slot> GetById(string slotId);
        Task Update(UpdateRequest updateRequest);
        Task Delete(string slotId);
    }
    public class SlotService : ISlotService
    {

        private readonly TheGarageContext _context;
        private readonly IMapper _mapper;

        public SlotService(TheGarageContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Slot> Register(RegisterRequest registerRequest)
        {
            var slot = _mapper.Map<Slot>(registerRequest);

            if (await GetById(slot.SlotId) == null)
                throw new AppException("Slot id \"" + slot.SlotId + "\" is registered");

            await _context.Slots.AddAsync(slot);
            await _context.SaveChangesAsync();

            return slot;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<IEnumerable<Slot>> GetAll()
        {
            var slots = await _context.Slots.ToListAsync();
            return slots;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<Slot> GetById(string slotId)
        {
            var slotFinded = await _context.Slots.FindAsync(slotId);
            return slotFinded;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {
            var slot = _mapper.Map<Slot>(updateRequest);

            var slotInDatabase = await GetById(updateRequest.SlotId);

            if (slotInDatabase == null)
                throw new AppException("Slot not found");

            
            // Update floor of slot if it has changed
            if (!string.IsNullOrWhiteSpace(slotInDatabase.Floor) && slot.Floor != slotInDatabase.Floor)
            {
                slotInDatabase.Floor = slot.Floor;
            }

            // Update floor of location if it has changed
            if (!string.IsNullOrWhiteSpace(slotInDatabase.Location) && slot.Location != slotInDatabase.Location)
            {
                slotInDatabase.Location = slot.Location;
            }

            // Update floor of slot status id if it has changed
            if (!string.IsNullOrWhiteSpace(slotInDatabase.SlotStatusId.ToString()) && slot.SlotStatusId != slotInDatabase.SlotStatusId)
            {
                slotInDatabase.SlotStatusId = slot.SlotStatusId;
            }

            _context.Slots.Update(slotInDatabase);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete(string slotId)
        {
            var slot = await GetById(slotId);
            if (slot != null)
            {
                _context.Slots.Remove(slot);
                await _context.SaveChangesAsync();
            }
        }
    }
}