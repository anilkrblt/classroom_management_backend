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
                    NotificationId = 1,
                    Message = "Welcome to the new semester!",
                    CreatedAt = new DateTime(2024, 12, 25),
                }
              
            );
        }
    }
}