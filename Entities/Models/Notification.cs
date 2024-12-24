using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
      [Table("Notification")]
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<NotificationRecipient> NotificationRecipients { get; set; }
    }

}