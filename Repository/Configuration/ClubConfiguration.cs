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
                    Name = "Mathematics Club",
                    PresidentId = 1,
                    ImageData = null 
                },
                new Club
                {
                    ClubId = 2,
                    Name = "Physics Club",
                    PresidentId = 2,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 3,
                    Name = "Chemistry Society",
                    PresidentId = 3,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 4,
                    Name = "Biology Club",
                    PresidentId = 4,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 5,
                    Name = "Computer Science Club",
                    PresidentId = 5,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 6,
                    Name = "Engineering Club",
                    PresidentId = 6,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 7,
                    Name = "Economics Club",
                    PresidentId = 7,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 8,
                    Name = "History Society",
                    PresidentId = 8,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 9,
                    Name = "Philosophy Club",
                    PresidentId = 9,
                    ImageData = null
                },
                new Club
                {
                    ClubId = 10,
                    Name = "Art and Design Club",
                    PresidentId = 10,
                    ImageData = null
                }
            );
        }
    }
}
