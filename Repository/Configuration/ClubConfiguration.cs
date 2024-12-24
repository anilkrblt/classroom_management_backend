using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.HasData
            (
            new Club
            {
                ClubId = 1,
                Name = "A",
                PresidentId = 2
            },
            new Club
            {
                ClubId = 2,
                Name = "B",
                PresidentId = 1
            }
            );
        }

     
    }
}