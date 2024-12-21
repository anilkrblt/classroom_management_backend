using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string? Name { get; set; }


    }
}