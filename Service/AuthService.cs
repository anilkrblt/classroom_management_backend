using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{


    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;


        public AuthService(UserManager<User> userManager, IConfiguration configuration,
                           IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public UserDto AuthenticateUser(string email, string password)
        {
            // Instructor tablosunda arama
            var instructor = _repositoryManager.Instructor.AuthenticateInstructor(email, password);

            if (instructor != null)
            {
                if (instructor.IsAdmin)
                {
                    return new UserDto
                    {
                        Email = instructor.Email,
                        Role = "admin",
                        IsAdmin = instructor.IsAdmin
                    };
                }
                else
                {
                    return new UserDto
                    {
                        Email = instructor.Email,
                        Role = "instructor",
                        IsAdmin = instructor.IsAdmin
                    };
                }

            }

            // Employee tablosunda arama
            var employee = _repositoryManager.Employee.AuthenticateEmployee(email, password);

            if (employee != null)
            {
                if (employee.IsAdmin)
                {
                    return new UserDto
                    {
                        Email = employee.Email,
                        Role = "admin",
                        IsAdmin = employee.IsAdmin
                    };
                }
                else
                {
                    return new UserDto
                    {
                        Email = employee.Email,
                        Role = "employee",
                        IsAdmin = employee.IsAdmin
                    };
                }

            }

            // Student tablosunda arama
            var student = _repositoryManager.Student.AuthenticateStudent(email, password);

            if (student != null)
            {
                if (student.IsClubManager)
                {
                    return new UserDto
                    {
                        Email = student.Email,
                        Role = "clubManagerStudent",
                        IsAdmin = false
                    };
                }
                else
                {
                    return new UserDto
                    {
                        Email = student.Email,
                        Role = "student",
                        IsAdmin = false
                    };
                }

            }

            throw new UnauthorizedAccessException("Invalid email or password.");
        }


        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user,
            userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }


        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);

            var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
            /*
            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password."); */
            if (!result)
                Console.WriteLine("validate user içersinde", result);
            return result;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName)
                };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }


    }

}