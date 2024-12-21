namespace Entities.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string? Name { get; set; }

        public required ICollection<Student> Students { get; set; }

        public required ICollection<Instructor> Instructors { get; set; }

        public required ICollection<Lecture> Lectures { get; set; }
        public required ICollection<Lab> Labs { get; set; }

        public required ICollection<Class> Classes { get; set; }

        public required ICollection<LectureHall> LectureHalls { get; set; }






    }
}