using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class NotificationRecipientConfiguration : IEntityTypeConfiguration<NotificationRecipient>
    {
        public void Configure(EntityTypeBuilder<NotificationRecipient> NotificationRecipient)
        {
        NotificationRecipient.HasData
        (
            new NotificationRecipient
            {
                Id = 1,
                NotificationId = 1,
                StudentId = 1
            }
          
        );
        }
    }
}