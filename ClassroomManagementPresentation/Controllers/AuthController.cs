using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shared.DataTransferObjects;
using Service.Contracts;

namespace ClassroomManagementPresentation.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto.Username == "test" && loginDto.Password == "password")
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDto.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

                var token = _tokenService.CreateToken(loginDto.Username, claims);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }

}
