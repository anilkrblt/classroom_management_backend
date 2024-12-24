using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> Notification)
        {
                 Notification.HasData
            (
                new Notification
                {
                    NotificationId = 101,
                    Message = "Welcome to the new semester!",
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Notification
                {
                    NotificationId = 102,
                    Message = "Reminder: Submit your assignments by the end of this week.",
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new Notification
                {
                    NotificationId = 103,
                    Message = "Your midterm results are now available.",
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                },
                new Notification
                {
                    NotificationId = 104,
                    Message = "Join us for the upcoming workshop on AI and Machine Learning.",
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                },
                new Notification
                {
                    NotificationId = 105,
                    Message = "Library hours extended during finals week.",
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Notification
                {
                    NotificationId = 106,
                    Message = "New internship opportunities are available. Check your emails for details.",
                    CreatedAt = DateTime.UtcNow.AddDays(-9)
                },
                new Notification
                {
                    NotificationId = 107,
                    Message = "Update: Campus Wi-Fi maintenance scheduled for tomorrow.",
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                },
                new Notification
                {
                    NotificationId = 108,
                    Message = "Congratulations to all the participants of the Hackathon!",
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Notification
                {
                    NotificationId = 109,
                    Message = "Your course registration is now confirmed.",
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Notification
                {
                    NotificationId = 110,
                    Message = "Don't miss the cultural fest this weekend!",
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            );
        }
    }
}