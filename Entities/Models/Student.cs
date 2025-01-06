using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("Student")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Range(1, 8)]
        public int GradeLevel { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ClubMembership> ClubMemberships { get; set; }
        public ICollection<ClubReservation> ClubReservations { get; set; }

        [Required]
        public string UserId { get; set; } // ForeignKey to User

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }


}