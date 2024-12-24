using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> reservation)
        {
             reservation.HasData
    (
        new Reservation
        {
            ReservationId = 1,
            EventDate = new DateTime(2024, 12, 25),
            StartTime = new TimeSpan(14, 0, 0),
            EndTime = new TimeSpan(16, 0, 0),
            LectureCode = "CS101",
            RoomId = 101,
            EventRegisterLink = "https://example.com/event1",
            ClubName = "Science Club",
            CreatedBy = 1
        },
        new Reservation
        {
            ReservationId = 2,
            EventDate = new DateTime(2024, 12, 26),
            StartTime = new TimeSpan(10, 0, 0),
            EndTime = new TimeSpan(12, 0, 0),
            LectureCode = "MATH102",
            RoomId = 102,
            EventRegisterLink = "https://example.com/event2",
            ClubName = "Math Club",
            CreatedBy = 2
        },
        new Reservation
        {
            ReservationId = 3,
            EventDate = new DateTime(2024, 12, 27),
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(11, 0, 0),
            LectureCode = "PHYS103",
            RoomId = 103,
            EventRegisterLink = "https://example.com/event3",
            ClubName = "Physics Club",
            CreatedBy = 3
        },
        new Reservation
        {
            ReservationId = 4,
            EventDate = new DateTime(2024, 12, 28),
            StartTime = new TimeSpan(13, 0, 0),
            EndTime = new TimeSpan(15, 0, 0),
            LectureCode = "BIO104",
            RoomId = 104,
            EventRegisterLink = "https://example.com/event4",
            ClubName = "Biology Club",
            CreatedBy = 4
        },
        new Reservation
        {
            ReservationId = 5,
            EventDate = new DateTime(2024, 12, 29),
            StartTime = new TimeSpan(11, 0, 0),
            EndTime = new TimeSpan(13, 0, 0),
            LectureCode = "CHEM105",
            RoomId = 105,
            EventRegisterLink = "https://example.com/event5",
            ClubName = "Chemistry Club",
            CreatedBy = 5
        }
    );
        }
    }
}