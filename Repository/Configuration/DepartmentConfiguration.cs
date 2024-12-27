using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> Department)
        {
            Department.HasData
            (
                new Department
                {
                    DepartmentId = 1,
                    Name = "Bilgisayar Mühendisliği",
                },
                new Department
                {
                    DepartmentId = 2,
                    Name = "Makine Mühendisliği",
                },
                new Department
                {
                    DepartmentId = 3,
                    Name = "Elektrik Elektronik Mühendisliği",
                },
                new Department
                {
                    DepartmentId = 4,
                    Name = "Biyo Mühendislik",
                },
                new Department
                {
                    DepartmentId = 5,
                    Name = "Gıda Mühendisliği",
                }
            );
        }
    }
}