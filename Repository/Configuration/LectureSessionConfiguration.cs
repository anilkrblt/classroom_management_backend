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
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    LectureCode = "MATH101",
                    RoomId = 1,
                    InstructorId = 1,
                    IsExtraLesson = 1,
                }
              
            );
        }
    }
}
