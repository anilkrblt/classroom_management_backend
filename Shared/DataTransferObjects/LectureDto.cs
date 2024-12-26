using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record LectureDto
    {

        public string Code { get; set; } = null!;

        public string? Name { get; set; }

        public string DepartmentName { get; set; }

        public string InstructorName { get; set; }



    }
}