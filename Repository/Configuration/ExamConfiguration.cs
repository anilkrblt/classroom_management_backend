using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasData
            (
                new Exam
                {
                    ExamId = 1,
                    Name = "Mathematics Final Exam",
                    IsFinal = false,
                    LectureCode = "MATH101"
                }
            );
        }
    }
}
