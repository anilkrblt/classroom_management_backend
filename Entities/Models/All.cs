using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagement.Models
{
    public enum RoomType
    {
        Classroom,
        Lab,
        LectureHall
    }

    public enum ReservationType
    {
        Regular,
        ClubActivity,
        LecturePostponement
    }

    [Table("Building")]
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }

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

    [Table("Room")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Range(0, 999)]
        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        public bool IsProjectorWorking { get; set; } // Indicates whether the projector is functional

        public string Equipment { get; set; } // JSON to store additional equipment details

        [ForeignKey("Building")]
        public int BuildingId { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Building Building { get; set; }
        public Department Department { get; set; }
        public ICollection<LectureSession> LectureSessions { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Request> Requests { get; set; }
    }

    [Table("Student")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Range(1, 8)]
        public int GradeLevel { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ClubMembership> ClubMemberships { get; set; }
    }

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

        public bool IsAdmin { get; set; } // Indicates if the instructor has admin privileges

        public Department Department { get; set; }
        public ICollection<LectureSession> LectureSessions { get; set; }
        public ICollection<InstructorPreference> InstructorPreferences { get; set; }
    }

    [Table("Lecture")]
    public class Lecture
    {
        [Key]
        [Required, StringLength(20)]
        public string Code { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<LectureSession> LectureSessions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }

    [Table("LectureSession")]
    public class LectureSession
    {
        [Key]
        public int LectureSessionId { get; set; }

        [Required, StringLength(20)]
        public string DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        public Lecture Lecture { get; set; }
        public Room Room { get; set; }
        public Instructor Instructor { get; set; }
    }

    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public ReservationType ReservationType { get; set; }

        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public int CreatedBy { get; set; }

        public Lecture Lecture { get; set; }
        public Room Room { get; set; }
    }

    [Table("Request")]
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string PhotoPath { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public int SubmittedBy { get; set; }

        public Room Room { get; set; }
    }

    [Table("Notification")]
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<NotificationRecipient> NotificationRecipients { get; set; }
    }

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

    [Table("Club")]
    public class Club
    {
        [Key]
        public int ClubId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("Student")]
        public int PresidentId { get; set; }

        public Student President { get; set; }
        public ICollection<Reservation> ClubReservations { get; set; }
        public ICollection<ClubMembership> ClubMemberships { get; set; }
    }

    [Table("ClubMembership")]
    public class ClubMembership
    {
        [Key]
        public int ClubMembershipId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }

        public Student Student { get; set; }
        public Club Club { get; set; }
    }

    [Table("Exam")]
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LectureCode { get; set; }

        [ForeignKey("Lecture")]
        public Lecture Lecture { get; set; }

        public ICollection<ExamSession> ExamSessions { get; set; }
    }

    [Table("ExamSession")]
    public class ExamSession
    {
        [Key]
        public int ExamSessionId { get; set; }

        [Required]
        public DateTime ExamDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        public Exam Exam { get; set; }
    }

    [Table("InstructorPreference")]
    public class InstructorPreference
    {
        [Key]
        public int PreferenceId { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }

        public string PreferredTime { get; set; } // JSON format for flexible time preferences

        public string UnavailableTimes { get; set; } // JSON format for unavailable times

        public Instructor Instructor { get; set; }
        public Lecture Lecture { get; set; }
    }

    [Table("Enrollment")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }

        public Student Student { get; set; }
        public Lecture Lecture { get; set; }
    }

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

        public bool IsAdmin { get; set; } // Indicates if the employee has admin privileges
    }
}
