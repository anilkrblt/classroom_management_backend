using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
            (

                new Employee
                {
                    EmployeeId = 1,
                    Name = "Ali Baba",
                    Email = "alibaba@trakya.edu.tr",
                    Password = "123",
                    IsAdmin = true
                },
                new Employee
                {
                    EmployeeId = 2,
                    Name = "Ayşe Yılmaz",
                    Email = "ayseyilmaz@trakya.edu.tr",
                    Password = "abc",
                    IsAdmin = false
                },
                new Employee
                {
                    EmployeeId = 3,
                    Name = "Mehmet Çelik",
                    Email = "mehmetcelik@trakya.edu.tr",
                    Password = "456",
                    IsAdmin = false
                },
                new Employee
                {
                    EmployeeId = 4,
                    Name = "Fatma Demir",
                    Email = "fatmademir@trakya.edu.tr",
                    Password = "789",
                    IsAdmin = false
                },
                new Employee
                {
                    EmployeeId = 5,
                    Name = "Ahmet Kaya",
                    Email = "ahmetkaya@trakya.edu.tr",
                    Password = "xyz",
                    IsAdmin = true
                },
                new Employee
                {
                    EmployeeId = 6,
                    Name = "Zeynep Aksoy",
                    Email = "zeynepaksoy@trakya.edu.tr",
                    Password = "321",
                    IsAdmin = false
                },
                new Employee
                {
                    EmployeeId = 7,
                    Name = "Hakan Yıldırım",
                    Email = "hakanyildirim@trakya.edu.tr",
                    Password = "654",
                    IsAdmin = false
                },
                new Employee
                {
                    EmployeeId = 8,
                    Name = "Selin Er",
                    Email = "seliner@trakya.edu.tr",
                    Password = "987",
                    IsAdmin = false
                },
                new Employee
                {
                    EmployeeId = 9,
                    Name = "Mustafa Kurt",
                    Email = "mustafakurt@trakya.edu.tr",
                    Password = "qwe",
                    IsAdmin = true
                },
                new Employee
                {
                    EmployeeId = 10,
                    Name = "Elif Sarı",
                    Email = "elifsari@trakya.edu.tr",
                    Password = "zxc",
                    IsAdmin = false
                }


            );
        }
    }
}