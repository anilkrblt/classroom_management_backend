﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace ClassroomManagement.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20241227081932_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Entities.Models.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("BuildingId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("Entities.Models.Club", b =>
                {
                    b.Property<int>("ClubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClubLogoPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("NameShortcut")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ClubId");

                    b.ToTable("Club");
                });

            modelBuilder.Entity("Entities.Models.ClubMembership", b =>
                {
                    b.Property<int>("ClubMembershipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClubId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("ClubMembershipId");

                    b.HasIndex("ClubId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClubMembership");
                });

            modelBuilder.Entity("Entities.Models.ClubReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Banner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ClubId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EventRegisterLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ReservationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClubReservation");
                });

            modelBuilder.Entity("Entities.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Entities.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LectureCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("LectureCode");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("Entities.Models.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LectureCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ExamId");

                    b.HasIndex("LectureCode");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("Entities.Models.ExamSession", b =>
                {
                    b.Property<int>("ExamSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExamDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ExamSessionId");

                    b.HasIndex("ExamId");

                    b.HasIndex("RoomId");

                    b.ToTable("ExamSession");
                });

            modelBuilder.Entity("Entities.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("InstructorId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("Entities.Models.InstructorPreference", b =>
                {
                    b.Property<int>("PreferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstructorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LectureCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PreferredTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UnavailableTimes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PreferenceId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("LectureCode");

                    b.ToTable("InstructorPreference");
                });

            modelBuilder.Entity("Entities.Models.Lecture", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Term")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Lecture");
                });

            modelBuilder.Entity("Entities.Models.LectureReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("InstructorId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLecturePostpone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReservationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.HasIndex("InstructorId");

                    b.HasIndex("ReservationId");

                    b.ToTable("LectureReservation");
                });

            modelBuilder.Entity("Entities.Models.LectureSession", b =>
                {
                    b.Property<int>("LectureSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("InstructorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsExtraLesson")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LectureCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("LectureTimes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("LectureSessionId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("LectureCode");

                    b.HasIndex("RoomId");

                    b.ToTable("LectureSession");
                });

            modelBuilder.Entity("Entities.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("NotificationId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("Entities.Models.NotificationRecipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NotificationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.HasIndex("StudentId");

                    b.ToTable("NotificationRecipient");
                });

            modelBuilder.Entity("Entities.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagePaths")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SolveDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RequestId");

                    b.HasIndex("RoomId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("Entities.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ReservationId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("Entities.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BuildingId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Equipment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ExamCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsProjectorWorking")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomType")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("Entities.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("GradeLevel")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsClubManager")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Entities.Models.ClubMembership", b =>
                {
                    b.HasOne("Entities.Models.Club", "Club")
                        .WithMany("ClubMemberships")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Student", "Student")
                        .WithMany("ClubMemberships")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entities.Models.ClubReservation", b =>
                {
                    b.HasOne("Entities.Models.Club", "Club")
                        .WithMany("ClubReservations")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Reservation", "Reservation")
                        .WithMany("ClubReservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Student", "Student")
                        .WithMany("ClubReservations")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Reservation");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entities.Models.Enrollment", b =>
                {
                    b.HasOne("Entities.Models.Lecture", "Lecture")
                        .WithMany()
                        .HasForeignKey("LectureCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entities.Models.Exam", b =>
                {
                    b.HasOne("Entities.Models.Lecture", "Lecture")
                        .WithMany("Exams")
                        .HasForeignKey("LectureCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("Entities.Models.ExamSession", b =>
                {
                    b.HasOne("Entities.Models.Exam", "Exam")
                        .WithMany("ExamSessions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Entities.Models.Instructor", b =>
                {
                    b.HasOne("Entities.Models.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Entities.Models.InstructorPreference", b =>
                {
                    b.HasOne("Entities.Models.Instructor", "Instructor")
                        .WithMany("InstructorPreferences")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Lecture", "Lecture")
                        .WithMany("InstructorPreferences")
                        .HasForeignKey("LectureCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("Entities.Models.Lecture", b =>
                {
                    b.HasOne("Entities.Models.Department", "Department")
                        .WithMany("Lectures")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Entities.Models.LectureReservation", b =>
                {
                    b.HasOne("Entities.Models.Lecture", "Lecture")
                        .WithMany("LectureReservations")
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Instructor", "Instructor")
                        .WithMany("LectureReservations")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Reservation", "Reservation")
                        .WithMany("LectureReservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Lecture");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Entities.Models.LectureSession", b =>
                {
                    b.HasOne("Entities.Models.Instructor", "Instructor")
                        .WithMany("LectureSessions")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Lecture", "Lecture")
                        .WithMany("LectureSessions")
                        .HasForeignKey("LectureCode");

                    b.HasOne("Entities.Models.Room", "Room")
                        .WithMany("LectureSessions")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Lecture");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Entities.Models.NotificationRecipient", b =>
                {
                    b.HasOne("Entities.Models.Notification", "Notification")
                        .WithMany("NotificationRecipients")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entities.Models.Request", b =>
                {
                    b.HasOne("Entities.Models.Room", "Room")
                        .WithMany("Requests")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Entities.Models.Reservation", b =>
                {
                    b.HasOne("Entities.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Entities.Models.Room", b =>
                {
                    b.HasOne("Entities.Models.Building", "Building")
                        .WithMany("Rooms")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Department", "Department")
                        .WithMany("Rooms")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Building");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Entities.Models.Student", b =>
                {
                    b.HasOne("Entities.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Entities.Models.Building", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Entities.Models.Club", b =>
                {
                    b.Navigation("ClubMemberships");

                    b.Navigation("ClubReservations");
                });

            modelBuilder.Entity("Entities.Models.Department", b =>
                {
                    b.Navigation("Instructors");

                    b.Navigation("Lectures");

                    b.Navigation("Rooms");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Entities.Models.Exam", b =>
                {
                    b.Navigation("ExamSessions");
                });

            modelBuilder.Entity("Entities.Models.Instructor", b =>
                {
                    b.Navigation("InstructorPreferences");

                    b.Navigation("LectureReservations");

                    b.Navigation("LectureSessions");
                });

            modelBuilder.Entity("Entities.Models.Lecture", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("InstructorPreferences");

                    b.Navigation("LectureReservations");

                    b.Navigation("LectureSessions");
                });

            modelBuilder.Entity("Entities.Models.Notification", b =>
                {
                    b.Navigation("NotificationRecipients");
                });

            modelBuilder.Entity("Entities.Models.Reservation", b =>
                {
                    b.Navigation("ClubReservations");

                    b.Navigation("LectureReservations");
                });

            modelBuilder.Entity("Entities.Models.Room", b =>
                {
                    b.Navigation("LectureSessions");

                    b.Navigation("Requests");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Entities.Models.Student", b =>
                {
                    b.Navigation("ClubMemberships");

                    b.Navigation("ClubReservations");

                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
