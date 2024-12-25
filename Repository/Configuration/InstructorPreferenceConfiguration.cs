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
                },
                new InstructorPreference
                {
                    PreferenceId = 2,
                    InstructorId = 2,
                    LectureCode = "PHYS201",
                    PreferredTime = "{ \"Tuesday\": [\"10:00-12:00\", \"14:00-16:00\"], \"Thursday\": [\"09:00-11:00\"] }",
                    UnavailableTimes = "{ \"Monday\": [\"08:00-10:00\"], \"Wednesday\": [\"15:00-17:00\"] }"
                },
                new InstructorPreference
                {
                    PreferenceId = 3,
                    InstructorId = 3,
                    LectureCode = "BIOL102",
                    PreferredTime = "{ \"Monday\": [\"13:00-15:00\"], \"Friday\": [\"09:00-11:00\"] }",
                    UnavailableTimes = "{ \"Tuesday\": [\"10:00-12:00\"] }"
                },
                new InstructorPreference
                {
                    PreferenceId = 4,
                    InstructorId = 4,
                    LectureCode = "CH401",
                    PreferredTime = "{ \"Wednesday\": [\"08:00-10:00\", \"12:00-14:00\"] }",
                    UnavailableTimes = "{ \"Thursday\": [\"09:00-11:00\"] }"
                },
                new InstructorPreference
                {
                    PreferenceId = 5,
                    InstructorId = 5,
                    LectureCode = "CS202",
                    PreferredTime = "{ \"Tuesday\": [\"14:00-16:00\"], \"Friday\": [\"08:00-10:00\"] }",
                    UnavailableTimes = "{ \"Monday\": [\"09:00-11:00\"] }"
                }
            );
        }
    }
}