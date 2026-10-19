using HotelBooking.Api.Dtos.Register;
using HotelBooking.Api.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService; 

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var register = await _authService.RegisterAsync(registerDto);

                return Ok(new { message = " USer Register  successfully", register });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});  
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var login = await _authService.LoginAsync(loginDto);

            if(login == null)
            {
                return Unauthorized(new {message = "Invaild email or password "});              
            }
            return Ok(login);
        }
    }  
}
