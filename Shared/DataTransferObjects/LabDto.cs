using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record LabDto
    {
        
        public int RoomId { get; set; }
        public required List<string> Equipment { get; set; }
        public int DepartmentId { get; set; }

    }
}