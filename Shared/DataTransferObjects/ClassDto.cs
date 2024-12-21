using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassDto
    {
        public bool ProjectorStatus { get; set; }

        public int RoomId { get; set; }
        public int DepartmentId { get; set; }
    }
}