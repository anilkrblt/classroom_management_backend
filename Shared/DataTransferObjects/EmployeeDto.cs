using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }

    public record EmployeeLoginDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}