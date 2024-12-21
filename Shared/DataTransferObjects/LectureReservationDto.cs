using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record LectureReservationDto
    {
        
        public int ReservationId { get; set; }
        public int InstructorId { get; set; }



        
    }
}