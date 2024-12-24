using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
            (
            new Student
            {
                StudentId = 1,
                Name = "Kemal",
                Email="kemal123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=1
            },
            new Student
            {
                StudentId = 2,
                Name = "Ahmet",
                Email="ahmet123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=2
            },
            new Student
            {
                StudentId = 3,
                Name = "Mustafa",
                Email="mustafa123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=3
            },
            new Student
            {
                StudentId = 4,
                Name = "Bülent",
                Email="bülent123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=4
            },
            new Student
            {
                StudentId = 5,
                Name = "Osman",
                Email="osman123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=5
            },
            new Student
            {
                StudentId = 6,
                Name = "Hakan",
                Email="hakan123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=1
            },
            new Student
            {
                StudentId = 7,
                Name = "Orhan",
                Email="orhan123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=2
            },
            new Student
            {
                StudentId = 8,
                Name = "Kenan",
                Email="kenan123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=3
            },
            new Student
            {
                StudentId = 10,
                Name = "Salih",
                Email="salih123@hotmail.com",
                Password="2131233A",
                GradeLevel=2,
                DepartmentId=4
            }
            );
        }
    }
}