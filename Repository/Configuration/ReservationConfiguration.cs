using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> reservation)
        {
            reservation.HasData
   (
       new Reservation
       {
           ReservationId = 1,
           StartTime = new TimeSpan(14, 0, 0),
           EndTime = new TimeSpan(16, 0, 0),
           EventDate = new DateTime(2024, 12, 25),
           RoomId = 1,
       },
             new Reservation
             {
                 ReservationId = 2,
                 StartTime = new TimeSpan(14, 0, 0),
                 EndTime = new TimeSpan(16, 0, 0),
                 EventDate = new DateTime(2024, 12, 25),
                 RoomId = 1,
               
             }

   );
        }
    }
}