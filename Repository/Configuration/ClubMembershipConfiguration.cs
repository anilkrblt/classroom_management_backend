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
                    ClubId = 1
                },
                new ClubMembership
                {
                    ClubMembershipId = 2,
                    StudentId = 2,
                    ClubId = 1
                },
                new ClubMembership
                {
                    ClubMembershipId = 3,
                    StudentId = 3,
                    ClubId = 2
                },
                new ClubMembership
                {
                    ClubMembershipId = 4,
                    StudentId = 4,
                    ClubId = 2
                },
                new ClubMembership
                {
                    ClubMembershipId = 5,
                    StudentId = 5,
                    ClubId = 3
                },
                new ClubMembership
                {
                    ClubMembershipId = 6,
                    StudentId = 6,
                    ClubId = 3
                },
                new ClubMembership
                {
                    ClubMembershipId = 7,
                    StudentId = 7,
                    ClubId = 4
                },
                new ClubMembership
                {
                    ClubMembershipId = 8,
                    StudentId = 8,
                    ClubId = 4
                },
                new ClubMembership
                {
                    ClubMembershipId = 9,
                    StudentId = 9,
                    ClubId = 5
                },
                new ClubMembership
                {
                    ClubMembershipId = 10,
                    StudentId = 10,
                    ClubId = 5
                }
            );
        }
    }
}
