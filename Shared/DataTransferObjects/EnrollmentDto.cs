using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record EnrollmentDto
    {

        public int StudentName { get; set; }

        public string LectureCode { get; set; }
    }
}