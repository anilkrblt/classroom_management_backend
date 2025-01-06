using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthService
    {

        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        public EmployeeLoginDto GetEmployeeData(string email);
        public InstructorLoginDto GetInstructorData(string email);
        public StudentLoginDto GetStudentData(string email);
        T GetUserData<T>(string email, Func<string, T> authenticateFunc) where T : class;
        Task MassPasswordResetAsync();

        Task<(bool IsValidUser, List<string> Roles)> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

    }

}