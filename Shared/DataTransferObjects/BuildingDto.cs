using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record BuildingDto
    {
        public int BuildingId { get; set; }

        public string? Name { get; set; }
    }

    public record BuildingForUpdateDto
    {
        public string? Name { get; set; }
    }
    public record BuildingForCreateDto
    {
        public string? Name { get; set; }
    }
}