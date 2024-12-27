using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class InstructorPreferenceConfiguration : IEntityTypeConfiguration<InstructorPreference>
    {
        public void Configure(EntityTypeBuilder<InstructorPreference> builder)
        {
            builder.HasData
            (
                new InstructorPreference
                {
                    PreferenceId = 1,
                    InstructorId = 1,
                    LectureCode = "MATH101",
                    PreferredTime = "{ \"Monday\": [\"09:00-11:00\", \"13:00-15:00\"], \"Wednesday\": [\"10:00-12:00\"] }",
                    UnavailableTimes = "{ \"Friday\": [\"08:00-10:00\", \"14:00-16:00\"] }"
                }
                
            );
        }
    }
}