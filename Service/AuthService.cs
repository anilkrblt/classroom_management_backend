using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
namespace Service
{


    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AuthService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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
                        Role = "Admin",
                        IsAdmin = instructor.IsAdmin
                    };
                }
                else
                {
                    return new UserDto
                    {
                        Email = instructor.Email,
                        Role = "Instructor",
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
                        Role = "Admin",
                        IsAdmin = employee.IsAdmin
                    };
                }
                else
                {
                    return new UserDto
                    {
                        Email = employee.Email,
                        Role = "Employee",
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
                        Role = "ClubManagerStudent",
                        IsAdmin = false
                    };
                }
                else
                {
                    return new UserDto
                    {
                        Email = student.Email,
                        Role = "Student",
                        IsAdmin = false
                    };
                }

            }

            throw new UnauthorizedAccessException("Invalid email or password.");
        }


    }

}