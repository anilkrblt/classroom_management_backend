using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace Shared.DataTransferObjects
{
    public record NotificationDto
    {
        public Guid UserId { get; set; }
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NotificationType { get; set; }
        public bool IsRead { get; set; }
    }
}
