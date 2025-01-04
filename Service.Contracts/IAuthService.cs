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
        UserDto AuthenticateUser(string email, string password);

        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);

        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

    }

}