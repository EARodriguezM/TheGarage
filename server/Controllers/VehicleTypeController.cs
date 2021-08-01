using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TheGarageAPI.Helpers;
using TheGarageAPI.Models.VehicleType;
using TheGarageAPI.Servicies;

namespace TheGarageAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        [Authorize(Roles = "1")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _vehicleTypeService.Register(registerRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicleTypes = await _vehicleTypeService.GetAll();
            return Ok(vehicleTypes);
        }

        [HttpGet("{vehicleTypeId}")]
        public IActionResult GetVehicleTypeById(string vehicleTypeId)
        {
            var vehicleType = _vehicleTypeService.GetVehicleTypeById(vehicleTypeId);

            if (vehicleType == null)
                return NotFound();

            return Ok(vehicleType);
        }

        [HttpPut("{vehicleTypeId}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            try
            {
                // Update vehicle
                _vehicleTypeService.Update(updateRequest);
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