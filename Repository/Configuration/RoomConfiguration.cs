using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> room)
        {
            room.HasData
            (
            new Room
            {
                RoomId = 1,
                Name = "L206",
                Capacity=88,
                ExamCapacity=40,
                IsActive=true,
                RoomType=RoomType.PcLab,
                IsProjectorWorking=true,
                Equipment="Bilgisayar,Sıra",
                BuildingId=1,
                DepartmentId=1
            },
            new Room
            {
                RoomId = 2,
                Name = "L208",
                Capacity=40,
                ExamCapacity=40,
                IsActive=true,
                RoomType=RoomType.PcLab,
                IsProjectorWorking=true,
                Equipment="Bilgisyar",
                BuildingId=1,
                DepartmentId=1
            },
            new Room
            {
                RoomId = 3,
                Name = "D201",
                Capacity=75,
                ExamCapacity=25,
                IsActive=true,
                RoomType=RoomType.Classroom,
                IsProjectorWorking=true,
                Equipment="Sıra",
                BuildingId=1,
                DepartmentId=2
            },
            new Room
            {
                RoomId = 4,
                Name = "D202",
                Capacity=78,
                ExamCapacity=28,
                IsActive=true,
                RoomType=RoomType.Classroom,
                IsProjectorWorking=true,
                Equipment="Sıra",
                BuildingId=1,
                DepartmentId=2
            },
            new Room
            {
                RoomId = 5,
                Name = "A1",
                Capacity=172,
                ExamCapacity=58,
                IsActive=true,
                RoomType=RoomType.Amfi,
                IsProjectorWorking=true,
                Equipment="sıra",
                BuildingId=2,
                DepartmentId=3
            },
            new Room
            {
                RoomId = 6,
                Name = "A2",
                Capacity=178,
                ExamCapacity=58,
                IsActive=true,
                RoomType=RoomType.Amfi,
                IsProjectorWorking=true,
                Equipment="sıra",
                BuildingId=2,
                DepartmentId=4
            },
            new Room
            {
                RoomId = 7,
                Name = "A3",
                Capacity=178,
                ExamCapacity=58,
                IsActive=true,
                RoomType=RoomType.Amfi,
                IsProjectorWorking=true,
                Equipment="sıra",
                BuildingId=2,
                DepartmentId=5
            },
            new Room
            {
                RoomId = 8,
                Name = "S1",
                Capacity=81,
                ExamCapacity=27,
                IsActive=true,
                RoomType=RoomType.Classroom,
                IsProjectorWorking=true,
                Equipment="sıra",
                BuildingId=2,
                DepartmentId=3
            },
            new Room
            {
                RoomId = 9,
                Name = "L104",
                Capacity=100,
                ExamCapacity=45,
                IsActive=true,
                RoomType=RoomType.Classroom,
                IsProjectorWorking=true,
                Equipment="sıra",
                BuildingId=1,
                DepartmentId=2
            },
            new Room
            {
                RoomId = 10,
                Name = "L204",
                Capacity=40,
                ExamCapacity=40,
                IsActive=true,
                RoomType=RoomType.Classroom,
                IsProjectorWorking=true,
                Equipment="sıra",
                BuildingId=1,
                DepartmentId=4
            }
            );
        }
    }
}