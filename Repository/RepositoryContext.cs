using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
    {

      
/*
// veri tabanında default veri yükleyerek başlatabilmek için
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }*/


        public required DbSet<Building> Buildings { get; set; }
        public required DbSet<Class> Classes { get; set; }

        public required DbSet<Club> Clubs { get; set; }

        public required DbSet<ClubReservation> ClubReservations { get; set; }

        public required DbSet<Department> Departments { get; set; }

        public required DbSet<Employee> Employees { get; set; }

        public required DbSet<Enrollment> Enrollments { get; set; }

        public required DbSet<Exam> Exams { get; set; }

        public required DbSet<ExamClass> ExamClasses { get; set; }

        public required DbSet<Instructor> Instructors { get; set; }

        public required DbSet<Lab> Labs { get; set; }

        public required DbSet<Lecture> Lectures { get; set; }

        public required DbSet<LectureHall> LectureHalls { get; set; }

        public required DbSet<LectureReservation> LectureReservations { get; set; }

        public required DbSet<LectureSession> LectureSessions { get; set; }

        public required DbSet<Request> Requests { get; set; }

        public required DbSet<Reservation> Reservations { get; set; }

        public required DbSet<Room> Rooms { get; set; }

        public required DbSet<Student> Students { get; set; }






    }
}