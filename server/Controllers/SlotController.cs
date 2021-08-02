using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TheGarageAPI.Helpers;
using TheGarageAPI.Models.Slot;
using TheGarageAPI.Servicies;

namespace TheGarageAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [Authorize(Roles = "1,2")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _slotService.Register(registerRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slots = await _slotService.GetAll();
            return Ok(slots);
        }

        [HttpGet("{slotId}")]
        public IActionResult GetById(string slotId)
        {
            // Only allow admins to access other user records
            var currentUserId = (User.Identity.Name).ToString();

            var slot = _slotService.GetById(slotId);

            if (slot == null)
                return NotFound();

            return Ok(slot);
        }

        [HttpPut("{slotId}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            try
            {
                // Update slot
                _slotService.Update(updateRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                // Return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{slotId}")]
        public async Task<IActionResult> Delete(string slotId)
        {
            try
            {
                await _slotService.Delete(slotId);
                return Ok();
            }
            catch (AppException ex)
            {
                // Return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}