using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System.Reflection;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        // Default veriler için OnModelCreating override edilebilir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                /*

            modelBuilder.Entity<LectureInstructor>()
                .HasOne(li => li.Lecture)
                .WithMany(l => l.LectureInstructors)
                .HasForeignKey(li => li.LectureCode)
                .OnDelete(DeleteBehavior.Cascade);
*/

            /*
                        base.OnModelCreating(modelBuilder);
                        modelBuilder.ApplyConfiguration(new BuildingConfiguration());
                        modelBuilder.ApplyConfiguration(new ClubConfiguration());
                        modelBuilder.ApplyConfiguration(new ClubMembershipConfiguration());
                        modelBuilder.ApplyConfiguration(new ClubReservationConfiguration());
                        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
                        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
                        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
                        modelBuilder.ApplyConfiguration(new ExamConfiguration());
                        modelBuilder.ApplyConfiguration(new ExamSessionConfiguration());
                        modelBuilder.ApplyConfiguration(new InstructorConfiguration());
                        modelBuilder.ApplyConfiguration(new InstructorPreferenceConfiguration());
                        modelBuilder.ApplyConfiguration(new LectureConfiguration());
                        modelBuilder.ApplyConfiguration(new LectureSessionConfiguration());
                        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
                        modelBuilder.ApplyConfiguration(new NotificationRecipientConfiguration());
                        modelBuilder.ApplyConfiguration(new RequestConfiguration());
                        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
                        modelBuilder.ApplyConfiguration(new RoomConfiguration());
                        modelBuilder.ApplyConfiguration(new StudentConfiguration());
            */
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
