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
                }
               
            );
        }
    }
}
