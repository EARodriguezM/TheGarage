

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TheGarageAPI.Helpers;
using TheGarageAPI.Models.Vehicle;
using TheGarageAPI.Servicies;

namespace TheGarageAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _vehicleService.Register(registerRequest);
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
            var vehicles = await _vehicleService.GetAll();
            return Ok(vehicles);
        }

        [HttpGet("{vehiclePlate}")]
        public IActionResult GetByPlate(string vehiclePlate)
        {
            // Only allow admins to access other user records
            var currentUserId = (User.Identity.Name).ToString();

            var vehicle = _vehicleService.GetByPlate(vehiclePlate);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPut("{vehiclePlate}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            try
            {
                // Update vehicle
                _vehicleService.Update(updateRequest);
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