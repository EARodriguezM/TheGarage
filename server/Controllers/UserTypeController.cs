using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TheGarageAPI.Helpers;
using TheGarageAPI.Models.UserType;
using TheGarageAPI.Servicies;

namespace TheGarageAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [Authorize(Roles = "1")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _userTypeService.Register(registerRequest);
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
            var userTypes = await _userTypeService.GetAll();
            return Ok(userTypes);
        }

        [AllowAnonymous]
        [HttpGet("{userTypeId}")]
        public IActionResult GetUserTypeById(string userTypeId)
        {
            var userType = _userTypeService.GetUserTypeById(userTypeId);

            if (userType == null)
                return NotFound();

            return Ok(userType);
        }

        [HttpPut("{userTypeId}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            try
            {
                // Update slot status
                _userTypeService.Update(updateRequest);
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