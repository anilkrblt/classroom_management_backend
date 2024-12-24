using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasData
            (
                new Instructor
                {
                    InstructorId = 1,
                    Name = "Altan Mesut",
                    Email = "altanmesut@trakya.edu.tr",
                    Password = "123",
                    Title = "Dr. Ögretim Üyesi",
                    DepartmentId = 1,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 2,
                    Name = "Emir Öztürk",
                    Email = "emirozturk@trakya.edu.tr",
                    Password = "123",
                    Title = "Dr. Ögretim Üyesi",
                    DepartmentId = 1,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 3,
                    Name = "Ayşe Kaya",
                    Email = "aysekaya@trakya.edu.tr",
                    Password = "456",
                    Title = "Doçent Doktor",
                    DepartmentId = 2,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 4,
                    Name = "Mehmet Yıldız",
                    Email = "mehmetyildiz@trakya.edu.tr",
                    Password = "789",
                    Title = "Profesör Doktor",
                    DepartmentId = 3,
                    IsAdmin = true,
                },
                new Instructor
                {
                    InstructorId = 5,
                    Name = "Fatma Demir",
                    Email = "fatmademir@trakya.edu.tr",
                    Password = "abc",
                    Title = "Doçent Doktor",
                    DepartmentId = 2,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 6,
                    Name = "Ahmet Çelik",
                    Email = "ahmetcelik@trakya.edu.tr",
                    Password = "xyz",
                    Title = "Dr. Ögretim Üyesi",
                    DepartmentId = 1,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 7,
                    Name = "Zeynep Arslan",
                    Email = "zeyneparslan@trakya.edu.tr",
                    Password = "qwe",
                    Title = "Dr. Ögretim Üyesi",
                    DepartmentId = 3,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 8,
                    Name = "Mustafa Özkan",
                    Email = "mustafaozkan@trakya.edu.tr",
                    Password = "zxc",
                    Title = "Dr. Ögretim Üyesi",
                    DepartmentId = 4,
                    IsAdmin = false,
                },
                new Instructor
                {
                    InstructorId = 9,
                    Name = "Elif Sarı",
                    Email = "elifsari@trakya.edu.tr",
                    Password = "poi",
                    Title = "Profesör Doktor",
                    DepartmentId = 3,
                    IsAdmin = true,
                },
                new Instructor
                {
                    InstructorId = 10,
                    Name = "Hakan Yılmaz",
                    Email = "hakanyilmaz@trakya.edu.tr",
                    Password = "mnb",
                    Title = "Doçent Doktor",
                    DepartmentId = 2,
                    IsAdmin = false,
                }
            );
        }
    }
}
