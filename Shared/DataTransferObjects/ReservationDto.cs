using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ReservationDto
    {
        public int ReservationId { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ApprovedBy { get; set; }

        public string? Description { get; set; }

        public int RoomId { get; set; }

    }
}