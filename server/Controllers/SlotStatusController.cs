using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TheGarageAPI.Helpers;
using TheGarageAPI.Models.SlotStatus;
using TheGarageAPI.Servicies;

namespace TheGarageAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SlotStatusController : ControllerBase
    {
        private readonly ISlotStatusService _slotStatusService;

        public SlotStatusController(ISlotStatusService slotStatusService)
        {
            _slotStatusService = slotStatusService;
        }

        [Authorize(Roles = "1")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _slotStatusService.Register(registerRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slotStatuses = await _slotStatusService.GetAll();
            return Ok(slotStatuses);
        }

        [AllowAnonymous]
        [HttpGet("{slotStatusId}")]
        public IActionResult GetSlotStatusById(string slotStatusId)
        {
            var slotStatus = _slotStatusService.GetSlotStatusById(slotStatusId);

            if (slotStatus == null)
                return NotFound();

            return Ok(slotStatus);
        }

        [HttpPut("{slotStatusId}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            try
            {
                // Update slot status
                _slotStatusService.Update(updateRequest);
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