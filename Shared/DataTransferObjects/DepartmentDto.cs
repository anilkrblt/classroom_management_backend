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

    public record DepartmentForCreateDto
    {

        public string? Name { get; set; }


    }

    public record DepartmentForUpdateDto
    {

        public string? Name { get; set; }


    }
}