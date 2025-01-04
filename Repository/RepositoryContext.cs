using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System.Reflection;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        // Default veriler için OnModelCreating override edilebilir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); 


            // LectureInstructor Many-to-Many ilişkisinin tanımlanması
            modelBuilder.Entity<LectureInstructor>()
                .HasKey(li => new { li.LectureCode, li.InstructorId });

            modelBuilder.Entity<LectureInstructor>()
                .HasOne(li => li.Lecture)
                .WithMany(l => l.LectureInstructors)
                .HasForeignKey(li => li.LectureCode);

            modelBuilder.Entity<LectureInstructor>()
                .HasOne(li => li.Instructor)
                .WithMany(i => i.LectureInstructors)
                .HasForeignKey(li => li.InstructorId);



        }



        public DbSet<Building>? Buildings { get; set; }
        public DbSet<Club>? Clubs { get; set; }
        public DbSet<Department>? Departments { get; set; }
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
        public DbSet<LectureReservation>? LectureReservations { get; set; }
        public DbSet<ClubReservation>? ClubReservations { get; set; }
        public DbSet<LectureInstructor> LectureInstructors { get; set; }

    }
}
