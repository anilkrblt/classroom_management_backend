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
                NotificationId = 101,
                StudentId = 1
            },
            new NotificationRecipient
            {
                Id = 2,
                NotificationId = 102,
                StudentId = 2
            },
            new NotificationRecipient
            {
                Id = 3,
                NotificationId = 103,
                StudentId = 3
            },
            new NotificationRecipient
            {
                Id = 4,
                NotificationId = 104,
                StudentId = 4
            },
            new NotificationRecipient
            {
                Id = 5,
                NotificationId = 105,
                StudentId = 5
            },
            new NotificationRecipient
            {
                Id = 6,
                NotificationId = 106,
                StudentId = 6
            },
            new NotificationRecipient
            {
                Id = 7,
                NotificationId = 107,
                StudentId = 7
            },
            new NotificationRecipient
            {
                Id = 8,
                NotificationId = 108,
                StudentId = 8
            },
            new NotificationRecipient
            {
                Id = 9,
                NotificationId = 109,
                StudentId = 9
            },
            new NotificationRecipient
            {
                Id = 10,
                NotificationId = 110,
                StudentId = 10
            }
        );
        }
    }
}