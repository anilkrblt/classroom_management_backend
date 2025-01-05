using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class LectureReservationConfiguration : IEntityTypeConfiguration<LectureReservation>
    {
        public void Configure(EntityTypeBuilder<LectureReservation> builder)
        {
            builder.HasData
            (
                new LectureReservation
                {
                    Id = 1,
                    Code = "MATH101",
                    ReservationId = 1,
                }

            );
        }
    }
}