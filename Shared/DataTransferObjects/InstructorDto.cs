using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record InstructorDto
    {

        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Title { get; set; }
        public string DepartmentName { get; set; }
        public List<UserLecturesDto> Schedule { get; set; }

    }
    public record InstructorLoginDto
    {

        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string Email { get; set; }
        public string? Title { get; set; }
        public string DepartmentName { get; set; }
        public List<UserLecturesDto> Schedule { get; set; }

    }
}