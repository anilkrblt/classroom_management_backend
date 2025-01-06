using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{

    [Table("NotificationRecipient")]
    public class NotificationRecipient
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }

        // Eğer bir "Users" tablonuz varsa
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        // Bildirimin okunma durumu
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }

}