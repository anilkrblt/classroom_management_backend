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
                    DepartmentId = 1
                },
                new Lecture
                {
                    Code = "PHYS201",
                    Name = "Physics I",
                    DepartmentId = 2
                },
                new Lecture
                {
                    Code = "CHEM301",
                    Name = "Chemistry",
                    DepartmentId = 3
                },
                new Lecture
                {
                    Code = "BIOL102",
                    Name = "Biology",
                    DepartmentId = 1
                },
                new Lecture
                {
                    Code = "CS202",
                    Name = "Computer Science",
                    DepartmentId = 1
                },
                new Lecture
                {
                    Code = "ENG103",
                    Name = "English Language",
                    DepartmentId = 2
                },
                new Lecture
                {
                    Code = "HIST205",
                    Name = "History",
                    DepartmentId = 1
                },
                new Lecture
                {
                    Code = "ENGM202",
                    Name = "Engineering Mechanics",
                    DepartmentId = 2
                },
                new Lecture
                {
                    Code = "ECON301",
                    Name = "Economics",
                    DepartmentId = 3
                },
                new Lecture
                {
                    Code = "PHIL101",
                    Name = "Philosophy",
                    DepartmentId = 2
                }
            );
        }
    }
}
