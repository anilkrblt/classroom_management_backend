using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ClubMembershipConfiguration : IEntityTypeConfiguration<ClubMembership>
    {
        public void Configure(EntityTypeBuilder<ClubMembership> builder)
        {
            builder.HasData
            (
                new ClubMembership
                {
                    ClubMembershipId = 1,
                    StudentId = 1,
                    ClubId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }

            );
        }
    }
}
