using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Shared.DataTransferObjects;

namespace CompanyEmployees.AutoMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*
            // xml üzerinden düzgün response almak için
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress,
                            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            /*
            CreateMap<Company, CompanyDto>()
                    .ForCtorParam("FullAddress", opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country))); */

            CreateMap<Employee, EmployeeDto>();




            // CREATE DTO -> ENTITY
            CreateMap<LectureCreateDto, Lecture>()
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.LectureInstructors, opt => opt.Ignore())
                .ForMember(dest => dest.LectureSessions, opt => opt.Ignore())
                .ForMember(dest => dest.Exams, opt => opt.Ignore())
                .ForMember(dest => dest.InstructorPreferences, opt => opt.Ignore())
                .ForMember(dest => dest.LectureReservations, opt => opt.Ignore());

            // ENTITY -> GET DTO (örnek)
            CreateMap<Lecture, LectureDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Department,
                           opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term))
                .ForMember(dest => dest.Instructors,
                           opt => opt.MapFrom(src => src.LectureInstructors
                               .Select(li => li.Instructor)));

            // Instructor -> InstructorForLectureDto
            CreateMap<Instructor, InstructorForLectureDto>()
                .ForMember(dest => dest.InstructorId,
                           opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.InstructorName,
                           opt => opt.MapFrom(src => src.Name));



            CreateMap<LectureDto, Lecture>()
                // Department string -> DepartmentId (manuel handle edeceğiniz için ignore)
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                // Instructors -> LectureInstructors (manuel handle, ignore)
                .ForMember(dest => dest.LectureInstructors, opt => opt.Ignore())
                // ... diğer navigation alanlarını da ignore edin
                ;





            CreateMap<Lecture, LectureDto>()
             // Diğer alanlar
             .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Department,
                        opt => opt.MapFrom(src => src.Department.Name))
             .ForMember(dest => dest.Grade,
                        opt => opt.MapFrom(src => src.Grade))
             .ForMember(dest => dest.Term,
                        opt => opt.MapFrom(src => src.Term))

             // Instructors alanını LectureInstructors üzerinden dolduruyoruz
             .ForMember(dest => dest.Instructors,
                        opt => opt.MapFrom(src => src.LectureInstructors
                            .Select(li => li.Instructor)
                        ));

            // Instructor -> InstructorForLectureDto
            CreateMap<Instructor, InstructorForLectureDto>()
                .ForMember(dest => dest.InstructorId,
                           opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.InstructorName,
                           opt => opt.MapFrom(src => src.Name));


            //CreateMap<Room, RoomDto>();
            CreateMap<Room, RoomDto>()
        .ForMember(dest => dest.DepartmentName,
                   opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : "No Department"))
        .ForMember(dest => dest.BuildingName,
                   opt => opt.MapFrom(src => src.Building.Name))
        .ForMember(dest => dest.Lectures,
                   opt => opt.MapFrom(src => src.LectureSessions.Select(ls => new LectureInfoDto
                   {
                       LectureName = ls.Lecture.Name,
                       TeacherName = ls.Instructor.Name,
                       StartTime = ls.StartTime,
                       EndTime = ls.EndTime,
                       DepartmentName = ls.Lecture.Department.Name,
                       DayOfWeek = ls.DayOfWeek
                   }).ToList()));

            CreateMap<LectureSession, LectureInfoDto>()
                .ForMember(dest => dest.LectureName, opt => opt.MapFrom(src => src.Lecture.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Instructor.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Lecture.Department.Name))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek));

            CreateMap<Student, StudentDto>();




            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.ImagePaths));





            CreateMap<ClubReservation, ClubReservationGetDto>()
             .ForMember(dest => dest.ClubName,
                        opt => opt.MapFrom(src => src.Club.Name))
             .ForMember(dest => dest.ClubLogo,
                        opt => opt.MapFrom(src => src.Club.ClubLogoPath))
             .ForMember(dest => dest.ClubRoomName,
                        opt => opt.MapFrom(src => src.Reservation.Room.Name))
             .ForMember(dest => dest.EventDate,
                        opt => opt.MapFrom(src =>
                            src.Reservation.EventDate.ToString("dd/MM/yyyy")))
             .ForMember(dest => dest.EventTime,
                        opt => opt.MapFrom(src =>
                            $"{src.Reservation.StartTime} - {src.Reservation.EndTime}"))
             .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => src.Title))
             .ForMember(dest => dest.Details,
                        opt => opt.MapFrom(src => src.Details))
             .ForMember(dest => dest.KatilimLinki,
                        opt => opt.MapFrom(src => src.EventRegisterLink))
             .ForMember(dest => dest.Banner,
                        opt => opt.MapFrom(src => src.Banner))
             .ForMember(dest => dest.FullName,
                        opt => opt.MapFrom(src => src.Student.FullName))
             .ForMember(dest => dest.StudentNo,
                        opt => opt.MapFrom(src => src.Student.StudentId))
             .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => src.Status
                        ));



            CreateMap<LectureSession, LectureSessionDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Exam, ExamDto>();
            CreateMap<Enrollment, EnrollmentDto>();


            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, DepartmentForCreateDto>();
            CreateMap<Department, DepartmentForUpdateDto>();



            CreateMap<Club, ClubDto>();


            CreateMap<Building, BuildingDto>();
            CreateMap<Building, BuildingForCreateDto>();
            CreateMap<Building, BuildingForUpdateDto>();
            CreateMap<BuildingForCreateDto, Building>();
            CreateMap<BuildingForUpdateDto, Building>();




            CreateMap<Notification, NotificationDto>();
            CreateMap<Reservation, ReservationDto>();

        }
    }
}