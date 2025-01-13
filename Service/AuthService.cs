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

        public InstructorLoginDto GetInstructorData(string email)
        {
            var instructor = _repositoryManager.Instructor.GetInstructorByEmail(email);

            if (instructor == null) return null;
            var instructorDto = _mapper.Map<InstructorDto>(instructor);


            return new InstructorLoginDto
            {
                Id = instructorDto.InstructorId,
                InstructorName = instructorDto.InstructorName,
                Email = instructorDto.Email,
                Title = instructorDto.Title,
                DepartmentName = instructorDto.DepartmentName,
                Schedule = instructorDto.Schedule
            };
        }


        public EmployeeLoginDto GetEmployeeData(string email)
        {
            var employee = _repositoryManager.Employee.GetEmployeeByEmail(email);

            if (employee == null) return null;

            return new EmployeeLoginDto
            {
                Id = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
            };
        }


        public StudentLoginDto GetStudentData(string email)
        {
            var student = _repositoryManager.Student.GetStudentByEmail(email);

            if (student == null) return null;
            var studentDto = _mapper.Map<StudentLoginDto>(student);
            studentDto.Id = student.StudentId;
            return studentDto;
        }

        public T GetUserData<T>(string email, Func<string, T> authenticateFunc) where T : class
        {
            var user = authenticateFunc(email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email.");

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


        public async Task<(bool IsValidUser, List<string> Roles, string userId)> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);


            var isValid = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
            Console.WriteLine("giriş durumu: ", isValid);
            if (!isValid)
            {
                return (false, [], "");
            }

            var userId = _user.Id.ToString();
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var rol in roles)
            {
                Console.WriteLine($"giriş durumu: {rol}" );

            }


            return (true, roles.ToList(), userId);
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


        public async Task MassPasswordResetAsync()
        {

            var allUsers = _userManager.Users.ToList();
            string newPassword = "0123456789";

            foreach (var user in allUsers)
            {
                if (string.IsNullOrEmpty(user.SecurityStamp))
                {
                    user.SecurityStamp = Guid.NewGuid().ToString();

                    // Değişikliği veritabanına kaydet
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        // Hata yönetimi
                        // örn: updateResult.Errors
                    }
                }
                // 1) Kullanıcı için reset token alıyoruz
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                // 2) ResetPasswordAsync ile yeni şifre atıyoruz
                var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (!resetResult.Succeeded)
                {

                }
            }
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