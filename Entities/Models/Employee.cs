using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }


        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        public string UserId { get; set; } // ForeignKey to User

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}