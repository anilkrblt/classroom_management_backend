namespace Entities.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string? Type { get; set; }

        public string? Content { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        public int RoomId { get; set; }
        public Instructor Instructor { get; set; }
        public Student Student { get; set; }

        public Room Room { get; set; }
    }
}