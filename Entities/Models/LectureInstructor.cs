using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("LectureInstructor")]
    public class LectureInstructor
    {
        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }
        public Lecture Lecture { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
