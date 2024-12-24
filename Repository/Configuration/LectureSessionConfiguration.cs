using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class LectureSessionConfiguration : IEntityTypeConfiguration<LectureSession>
    {
        public void Configure(EntityTypeBuilder<LectureSession> builder)
        {
            builder.HasData
            (
                new LectureSession
                {
                    LectureSessionId = 1,
                    DayOfWeek = "Monday",
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    LectureCode = "MATH101",
                    RoomId = 1,
                    InstructorId = 1
                },
                new LectureSession
                {
                    LectureSessionId = 2,
                    DayOfWeek = "Tuesday",
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(12, 30, 0),
                    LectureCode = "PHYS201",
                    RoomId = 2,
                    InstructorId = 2
                },
                new LectureSession
                {
                    LectureSessionId = 3,
                    DayOfWeek = "Wednesday",
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(14, 30, 0),
                    LectureCode = "CHEM301",
                    RoomId = 3,
                    InstructorId = 3
                },
                new LectureSession
                {
                    LectureSessionId = 4,
                    DayOfWeek = "Thursday",
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(11, 30, 0),
                    LectureCode = "BIOL102",
                    RoomId = 4,
                    InstructorId = 4
                },
                new LectureSession
                {
                    LectureSessionId = 5,
                    DayOfWeek = "Friday",
                    StartTime = new TimeSpan(15, 0, 0),
                    EndTime = new TimeSpan(16, 30, 0),
                    LectureCode = "CS202",
                    RoomId = 5,
                    InstructorId = 5
                },
                new LectureSession
                {
                    LectureSessionId = 6,
                    DayOfWeek = "Monday",
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(9, 30, 0),
                    LectureCode = "ENG103",
                    RoomId = 6,
                    InstructorId = 6
                },
                new LectureSession
                {
                    LectureSessionId = 7,
                    DayOfWeek = "Tuesday",
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(15, 30, 0),
                    LectureCode = "HIST205",
                    RoomId = 7,
                    InstructorId = 7
                },
                new LectureSession
                {
                    LectureSessionId = 8,
                    DayOfWeek = "Wednesday",
                    StartTime = new TimeSpan(9, 30, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    LectureCode = "ENGM202",
                    RoomId = 8,
                    InstructorId = 8
                },
                new LectureSession
                {
                    LectureSessionId = 9,
                    DayOfWeek = "Thursday",
                    StartTime = new TimeSpan(16, 0, 0),
                    EndTime = new TimeSpan(17, 30, 0),
                    LectureCode = "ECON301",
                    RoomId = 9,
                    InstructorId = 9
                },
                new LectureSession
                {
                    LectureSessionId = 10,
                    DayOfWeek = "Friday",
                    StartTime = new TimeSpan(12, 0, 0),
                    EndTime = new TimeSpan(13, 30, 0),
                    LectureCode = "PHIL101",
                    RoomId = 10,
                    InstructorId = 10
                }
            );
        }
    }
}
