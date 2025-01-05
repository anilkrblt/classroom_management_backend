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

        public InstructorLoginDto AuthenticateInstructor(string email, string password)
        {
            var instructor = _repositoryManager.Instructor.AuthenticateInstructor(email, password);

            if (instructor == null) return null;
            var instructorDto = _mapper.Map<InstructorDto>(instructor);


            return new InstructorLoginDto
            {
                InstructorId = instructorDto.InstructorId,
                InstructorName = instructorDto.InstructorName,
                Email = instructorDto.Email,
                Title = instructorDto.Title,
                DepartmentName = instructorDto.DepartmentName,
                Schedule = instructorDto.Schedule
            };
        }


        public EmployeeLoginDto AuthenticateEmployee(string email, string password)
        {
            var employee = _repositoryManager.Employee.AuthenticateEmployee(email, password);

            if (employee == null) return null;

            return new EmployeeLoginDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
            };
        }


        public StudentLoginDto AuthenticateStudent(string email, string password)
        {
            var student = _repositoryManager.Student.AuthenticateStudent(email, password);

            if (student == null) return null;
            var studentDto = _mapper.Map<StudentLoginDto>(student);
            return studentDto;
        }

        public T AuthenticateUser<T>(string email, string password, Func<string, string, T> authenticateFunc) where T : class
        {
            var user = authenticateFunc(email, password);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            return user;
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


        public async Task<(bool IsValidUser, List<string> Roles)> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);

            var isValid = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);

            if (!isValid)
            {
                Console.WriteLine("validate user içersinde", isValid);
                return (false, null);
            }

            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(_user);

            return (true, roles.ToList());
        }







        private async Task<List<Claim>> GetClaims()
        {

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim(ClaimTypes.Email, _user.Email)
                };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
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