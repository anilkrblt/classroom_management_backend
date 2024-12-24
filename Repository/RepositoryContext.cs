using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        // Constructor
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        // Default veriler için OnModelCreating override edilebilir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurations (Opsiyonel: Eğer konfigürasyonlar varsa)
            // modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            // modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public DbSet<Building>? Buildings { get; set; }
        public  DbSet<Club>? Clubs { get; set; }
        public  DbSet<Department>? Departments { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Enrollment>? Enrollments { get; set; }
        public DbSet<Exam>? Exams { get; set; }
        public DbSet<Instructor>? Instructors { get; set; }
        public DbSet<Lecture>? Lectures { get; set; }
        public DbSet<LectureSession>? LectureSessions { get; set; }
        public DbSet<Request>? Requests { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<ClubMembership>? ClubMemberships { get; set; }
        public DbSet<InstructorPreference>? InstructorPreferences { get; set; }
        public DbSet<NotificationRecipient>? NotificationRecipients { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<ExamSession>? ExamSessions { get; set; }


    }
}
