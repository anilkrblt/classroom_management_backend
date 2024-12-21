using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record EnrollmentDto
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public string Code { get; set; }
    }
}