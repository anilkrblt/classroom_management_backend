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
        public EmployeeLoginDto AuthenticateEmployee(string email, string password);
        public InstructorLoginDto AuthenticateInstructor(string email, string password);
        public StudentLoginDto AuthenticateStudent(string email, string password);
        T AuthenticateUser<T>(string email, string password, Func<string, string, T> authenticateFunc) where T : class;

        Task<(bool IsValidUser, List<string> Roles)> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

    }

}