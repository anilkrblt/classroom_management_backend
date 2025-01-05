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

            // Employee - User ilişki
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId);

            // Student - User ilişki
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId);

            // Instructor - User ilişki
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<Instructor>(i => i.UserId);

            // Reservation -> ClubReservation ilişkisi
            modelBuilder.Entity<ClubReservation>()
                .HasOne(cr => cr.Reservation)
                .WithMany(r => r.ClubReservations)
                .HasForeignKey(cr => cr.ReservationId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Reservation -> LectureReservation ilişkisi
            modelBuilder.Entity<LectureReservation>()
                .HasOne(lr => lr.Reservation)
                .WithMany(r => r.LectureReservations)
                .HasForeignKey(lr => lr.ReservationId)
                .OnDelete(DeleteBehavior.Cascade); 



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
