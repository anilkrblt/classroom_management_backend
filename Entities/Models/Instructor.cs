using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public bool IsAdmin { get; set; }

        public Department Department { get; set; }
        public ICollection<LectureSession> LectureSessions { get; set; }
        public ICollection<InstructorPreference> InstructorPreferences { get; set; }
        public ICollection<LectureReservation> LectureReservations { get; set; }
        public ICollection<LectureInstructor> LectureInstructors { get; set; }


    }
}