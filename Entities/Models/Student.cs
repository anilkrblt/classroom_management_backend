namespace Entities.Models
{
    public class Student
    {
        
        public int StudentId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public int DepartmentId { get; set; }
        public required Department Department { get; set; }


        public required ICollection<Club> Clubs { get; set; }

        public required ICollection<Enrollment> Enrollments { get; set; }



    }
}