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
        private readonly IServiceManager _serviceManager;

        public AuthController(ITokenService tokenService, IServiceManager serviceManager)
        {
            _tokenService = tokenService;
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // Kullanıcıyı doğrula
                var user = _serviceManager.AuthService.AuthenticateUser(loginDto.Email, loginDto.Password);

                // Token oluştur
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToLowerInvariant()),
            };

                var token = _tokenService.CreateToken(user.Email, claims);

                return Ok(new
                {
                    Token = token,
                    Role = user.Role,
                    IsAdmin = user.IsAdmin
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }


}
