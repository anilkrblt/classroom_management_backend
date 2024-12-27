using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasData
            (
                new Lecture
                {
                    Code = "MATH101",
                    Name = "Mathematics I",
                    Grade = 3,
                    Term = "Güz",
                    DepartmentId = 1
                }
               
            );
        }
    }
}
