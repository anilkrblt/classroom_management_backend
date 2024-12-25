using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> Request)
        {
        Request.HasData
        (
            new Request
            {
                RequestId = 1,
                Type = "Maintenance",
                Content = "Fix the projector in Room 101",
                Status = "Pending",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 1,
                SubmittedBy = 1
            },
            new Request
            {
                RequestId = 2,
                Type = "Cleaning",
                Content = "Clean the whiteboard in Room 102",
                Status = "Approved",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 2,
                SubmittedBy = 2
            },
            new Request
            {
                RequestId = 3,
                Type = "Equipment",
                Content = "Replace the chairs in Room 103",
                Status = "Completed",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 3,
                SubmittedBy = 3
            },
            new Request
            {
                RequestId = 4,
                Type = "IT Support",
                Content = "Internet not working in Room 104",
                Status = "Pending",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 4,
                SubmittedBy = 4
            },
            new Request
            {
                RequestId = 5,
                Type = "Furniture",
                Content = "Add a table to Room 105",
                Status = "Approved",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 5,
                SubmittedBy = 5
            },
            new Request
            {
                RequestId = 6,
                Type = "HVAC",
                Content = "Fix the air conditioning in Room 106",
                Status = "Pending",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 6,
                SubmittedBy = 6
            },
            new Request
            {
                RequestId = 7,
                Type = "Audio",
                Content = "Microphone not working in Room 107",
                Status = "Completed",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 7,
                SubmittedBy = 7
            },
            new Request
            {
                RequestId = 8,
                Type = "Lighting",
                Content = "Replace the bulbs in Room 108",
                Status = "Pending",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 8,
                SubmittedBy = 8
            },
            new Request
            {
                RequestId = 9,
                Type = "Seating",
                Content = "Add extra chairs to Room 109",
                Status = "Approved",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 9,
                SubmittedBy = 9
            },
            new Request
            {
                RequestId = 10,
                Type = "Security",
                Content = "Install a lock in Room 110",
                Status = "Pending",
                PhotoPath = new byte[] {},
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 10,
                SubmittedBy = 10
            }
        );
        }
    }
}