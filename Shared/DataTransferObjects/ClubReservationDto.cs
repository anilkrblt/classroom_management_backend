using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClubReservationDto
    {
        public int ReservationId { get; set; }

        public int ClubId { get; set; }

    }
}