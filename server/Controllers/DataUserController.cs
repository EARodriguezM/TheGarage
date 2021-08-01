using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheGarageAPI.Helpers;
using TheGarageAPI.Models.DataUser;
using TheGarageAPI.Servicies;

namespace TheGarageAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DataUserController : ControllerBase
    {
        //Do global the interface of data user service
        private readonly IDataUserService _dataUserService;

        public DataUserController(IDataUserService dataUserService)
        {
            _dataUserService = dataUserService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromForm]AuthenticateRequest authenticateRequest)
        {
            var user = await _dataUserService.Authenticate(authenticateRequest);

            if (user == null) 
                return BadRequest(new {message = "Email or password is incorrect"});

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _dataUserService.Register(registerRequest);
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
            var users =  await _dataUserService.GetAll();
            return Ok(users);
        }

        [HttpGet("{dataUserId}")]
        public IActionResult GetById(string dataUserId)
        {
            // Only allow admins to access other user records
            var currentUserId = (User.Identity.Name).ToString();
            
            if (dataUserId != currentUserId || !User.IsInRole("1"))
                return Forbid();

            var user =  _dataUserService.GetById(dataUserId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{dataUserId}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            
            // Only allow admins to access other user records
            var currentUserId = (User.Identity.Name).ToString();
            
            if (updateRequest.DataUserId != currentUserId || !User.IsInRole("1"))
                return Forbid();

            try
            {
                // Update user 
                _dataUserService.Update(updateRequest);
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