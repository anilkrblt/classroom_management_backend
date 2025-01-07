using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Entities.Models;

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
            var (IsValidUser, Roles, userId) = await _serviceManager.AuthService.ValidateUser(userForAuth);


            if (!IsValidUser)
            {
                return Unauthorized();
            }
            var userNotifs =await _serviceManager.NotificationService.GetAllNotificationsWithUserIdAsync(userId, false);
            userNotifs = userNotifs.ToList();
            if (Roles.Contains("Instructor"))
            {
                var instructor = _serviceManager.AuthService.GetUserData(userForAuth.Email, _serviceManager.AuthService.GetInstructorData);
                return Ok(new { Token = await _serviceManager.AuthService.CreateToken(),  Notifications = userNotifs, User = instructor });
            }
            else if (Roles.Contains("Employee"))
            {
                var employee = _serviceManager.AuthService.GetUserData(userForAuth.Email, _serviceManager.AuthService.GetEmployeeData);
                return Ok(new { Token = await _serviceManager.AuthService.CreateToken(), User = employee, Notifications = userNotifs });
            }
            else if (Roles.Contains("Student"))
            {
                var student = _serviceManager.AuthService.GetUserData(userForAuth.Email, _serviceManager.AuthService.GetStudentData);
                return Ok(new { Token = await _serviceManager.AuthService.CreateToken(), User = student, Notifications = userNotifs });
            }

            return Unauthorized("rol yok");
        }

        [HttpPost("/resetpasswords")]
        public async Task<IActionResult> ResetPasswords()
        {
            await _serviceManager.AuthService.MassPasswordResetAsync();
            return Ok();
        }

    }


}
