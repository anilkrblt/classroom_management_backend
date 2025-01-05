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
       
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
       
            _serviceManager = serviceManager;
        }
    
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await
           _serviceManager.AuthService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuth)
        {
            var result = await _serviceManager.AuthService.ValidateUser(userForAuth);
            if (!result.IsValidUser)
            {
                return Unauthorized();
            }
            if (result.Roles.Contains("Instructor"))
            {
                var instructor = _serviceManager.AuthService.AuthenticateUser(userForAuth.Email, userForAuth.Password, _serviceManager.AuthService.AuthenticateInstructor);
                return Ok(new { Token = await _serviceManager.AuthService.CreateToken(), User = instructor });
            }
            else if (result.Roles.Contains("Employee"))
            {
                var employee = _serviceManager.AuthService.AuthenticateUser(userForAuth.Email, userForAuth.Password, _serviceManager.AuthService.AuthenticateEmployee);
                return Ok(new { Token = await _serviceManager.AuthService.CreateToken(), User = employee });
            }
            else if (result.Roles.Contains("Student"))
            {
                var student = _serviceManager.AuthService.AuthenticateUser(userForAuth.Email, userForAuth.Password, _serviceManager.AuthService.AuthenticateStudent);
                return Ok(new { Token = await _serviceManager.AuthService.CreateToken(), User = student });
            }

            return Unauthorized();
        }

    }


}
