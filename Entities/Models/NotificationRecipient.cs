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

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Notification Notification { get; set; }
        public Student Student { get; set; }
    }
}