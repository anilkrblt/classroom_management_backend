using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ClubReservationConfiguration : IEntityTypeConfiguration<ClubReservation>
    {
        public void Configure(EntityTypeBuilder<ClubReservation> builder)
        {
            builder.HasData
            (
                new ClubReservation
                {
                    Id = 1,
                    ClubId = 1,
                    ReservationId = 2,
                    EventRegisterLink = "www.asasa.com",
                    EventName = "First event",
                    Title = "hos geldiniz efendim",
                    Details = "detaylandık",
                    Banner = "www.asdasa.com"
                }


            );
        }
    }
}