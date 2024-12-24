using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
    }
}