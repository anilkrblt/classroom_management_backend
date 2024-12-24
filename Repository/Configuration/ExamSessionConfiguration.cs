using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ExamSessionConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.HasData
            (
                new ExamSession
                {
                    ExamSessionId = 1,
                    ExamDate = new DateTime(2024, 6, 10),
                    DayOfWeek = "Monday",
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    RoomId = 1,
                    ExamId = 1
                },
                new ExamSession
                {
                    ExamSessionId = 2,
                    ExamDate = new DateTime(2024, 6, 11),
                    DayOfWeek = "Tuesday",
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(15, 0, 0),
                    RoomId = 2,
                    ExamId = 2
                },
                new ExamSession
                {
                    ExamSessionId = 3,
                    ExamDate = new DateTime(2024, 6, 12),
                    DayOfWeek = "Wednesday",
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    RoomId = 3,
                    ExamId = 3
                },
                new ExamSession
                {
                    ExamSessionId = 4,
                    ExamDate = new DateTime(2024, 6, 13),
                    DayOfWeek = "Thursday",
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    RoomId = 4,
                    ExamId = 4
                },
                new ExamSession
                {
                    ExamSessionId = 5,
                    ExamDate = new DateTime(2024, 6, 14),
                    DayOfWeek = "Friday",
                    StartTime = new TimeSpan(8, 30, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    RoomId = 5,
                    ExamId = 5
                },
                new ExamSession
                {
                    ExamSessionId = 6,
                    ExamDate = new DateTime(2024, 6, 15),
                    DayOfWeek = "Saturday",
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(13, 0, 0),
                    RoomId = 6,
                    ExamId = 6
                },
                new ExamSession
                {
                    ExamSessionId = 7,
                    ExamDate = new DateTime(2024, 6, 16),
                    DayOfWeek = "Sunday",
                    StartTime = new TimeSpan(15, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    RoomId = 7,
                    ExamId = 7
                },
                new ExamSession
                {
                    ExamSessionId = 8,
                    ExamDate = new DateTime(2024, 6, 17),
                    DayOfWeek = "Monday",
                    StartTime = new TimeSpan(9, 30, 0),
                    EndTime = new TimeSpan(11, 30, 0),
                    RoomId = 8,
                    ExamId = 8
                },
                new ExamSession
                {
                    ExamSessionId = 9,
                    ExamDate = new DateTime(2024, 6, 18),
                    DayOfWeek = "Tuesday",
                    StartTime = new TimeSpan(14, 30, 0),
                    EndTime = new TimeSpan(16, 30, 0),
                    RoomId = 9,
                    ExamId = 9
                },
                new ExamSession
                {
                    ExamSessionId = 10,
                    ExamDate = new DateTime(2024, 6, 19),
                    DayOfWeek = "Wednesday",
                    StartTime = new TimeSpan(12, 0, 0),
                    EndTime = new TimeSpan(14, 0, 0),
                    RoomId = 10,
                    ExamId = 10
                }
            );
        }
    }
}
