using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClubDto
    {
        public int ClubId { get; set; }

        public string? Name { get; set; }


        public int StudentId { get; set; }
    }

}