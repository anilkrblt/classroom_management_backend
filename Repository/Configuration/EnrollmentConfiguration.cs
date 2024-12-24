using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasData
            (
                new Enrollment
                {
                    EnrollmentId = 1,
                    StudentId = 1,
                    LectureCode = "MATH101"
                },
                new Enrollment
                {
                    EnrollmentId = 2,
                    StudentId = 2,
                    LectureCode = "PHYS201"
                },
                new Enrollment
                {
                    EnrollmentId = 3,
                    StudentId = 3,
                    LectureCode = "CHEM301"
                },
                new Enrollment
                {
                    EnrollmentId = 4,
                    StudentId = 4,
                    LectureCode = "BIOL102"
                },
                new Enrollment
                {
                    EnrollmentId = 5,
                    StudentId = 5,
                    LectureCode = "CS202"
                },
                new Enrollment
                {
                    EnrollmentId = 6,
                    StudentId = 6,
                    LectureCode = "ENG103"
                },
                new Enrollment
                {
                    EnrollmentId = 7,
                    StudentId = 7,
                    LectureCode = "HIST205"
                },
                new Enrollment
                {
                    EnrollmentId = 8,
                    StudentId = 8,
                    LectureCode = "ENGM202"
                },
                new Enrollment
                {
                    EnrollmentId = 9,
                    StudentId = 9,
                    LectureCode = "ECON301"
                },
                new Enrollment
                {
                    EnrollmentId = 10,
                    StudentId = 10,
                    LectureCode = "PHIL101"
                }
            );
        }
    }
}
