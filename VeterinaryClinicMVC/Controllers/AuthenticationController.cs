using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VeterinaryClinicMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // POST: api/authentication/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Logic for user registration goes here
            return Ok(); // Return appropriate response
        }

        // POST: api/authentication/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Logic for user login goes here
            return Ok(); // Return appropriate response
        }

        // POST: api/authentication/2fa
        [HttpPost("2fa")]
        public async Task<IActionResult> TwoFactorAuthentication([FromBody] TwoFactorRequest request)
        {
            // Logic for 2FA validation goes here
            return Ok(); // Return appropriate response
        }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TwoFactorRequest
    {
        public string Email { get; set; }
        public string TwoFactorCode { get; set; }
    }
}