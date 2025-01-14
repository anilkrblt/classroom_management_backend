using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Shared.DataTransferObjects;

namespace ClassroomManagement.MapperProfiles
{
    public class MappingProfile : Profile
    {
        private static string GetClubManagerFullName(IEnumerable<ClubMembership> memberships)
        {
            var manager = memberships.FirstOrDefault(m => m.IsClubManager);
            return manager?.Student.FullName ?? string.Empty; // Null kontrolü
        }

        private static int? GetClubManagerId(IEnumerable<ClubMembership> memberships)
        {
            var manager = memberships.FirstOrDefault(m => m.IsClubManager);
            return manager?.Student.StudentId; // Null kontrolü
        }

        public MappingProfile()
        {



            // ReservationDto -> Reservation
            CreateMap<ClubReservationDto, Reservation>()
                .ForMember(dest => dest.EventDate,
                           opt => opt.MapFrom(src => src.EventTime.Date))
                .ForMember(dest => dest.RoomId, opt => opt.Ignore());
            // RoomId manuel set edilecek

            // Dto -> ClubReservation
            CreateMap<ClubReservationDto, ClubReservation>()
                .ForMember(dest => dest.EventRegisterLink, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Banner, opt => opt.MapFrom(src => src.BannerPath))
                .ForMember(dest => dest.Reservation, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore())
                .ForMember(dest => dest.ClubId, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDto>();



            // PUT için RoomUpdateForBuildingDto -> Room
            CreateMap<RoomUpdateForBuildingDto, Room>()
                // RoomType int -> Enum
                .ForMember(dest => dest.RoomType,
                           opt => opt.MapFrom(src => (RoomType)src.RoomType))
                // Equipment null gelirse "" (empty) atayalım:
                .ForMember(dest => dest.Equipment,
                           opt => opt.MapFrom(src => src.Equipment ?? string.Empty));


            // CREATE: RoomCreationForBuildingDto -> Room
            CreateMap<RoomCreationForBuildingDto, Room>()
                .ForMember(dest => dest.BuildingId,
                    opt => opt.MapFrom(src => src.BuildingId))
                .ForMember(dest => dest.DepartmentId,
                    opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.RoomType,
                    opt => opt.MapFrom(src => (RoomType)src.RoomType))
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment));



            CreateMap<LectureUpdateDto, Lecture>();

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
        .ForMember(dest => dest.ClubEvents,
                    opt => opt.MapFrom(src => src.Reservations.SelectMany(r => r.ClubReservations
                    .Select(cr => new ClubEventDto
                    {
                        ReservationId = cr.ReservationId,
                        Banner = cr.Banner,
                        ClubLogo = cr.Club.ClubLogoPath,
                        ClubName = cr.Club.Name,
                        ClubShortcut = cr.Club.NameShortcut,
                        Details = cr.Details,
                        StartTime = r.StartTime,
                        EndTime = r.EndTime,
                        EventDate = r.EventDate,
                        Title = cr.Title,
                        Link = cr.EventRegisterLink
                    }))))
        .ForMember(dest => dest.Lectures,
                   opt => opt.MapFrom(src => src.LectureSessions.Select(ls => new LectureInfoDto
                   {
                       LectureSessionId = ls.LectureSessionId,
                       LectureCode = ls.Lecture.Code,
                       LectureName = ls.Lecture.Name,
                       TeacherName = ls.Instructor.Name,
                       StartTime = ls.StartTime,
                       EndTime = ls.EndTime,
                       DepartmentName = ls.Lecture.Department.Name,
                       Date = ls.Date,
                       IsExtraLesson = ls.IsExtraLesson
                   }).ToList()));

            CreateMap<LectureSession, LectureInfoDto>()
                .ForMember(dest => dest.LectureName, opt => opt.MapFrom(src => src.Lecture.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Instructor.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Lecture.Department.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));



            CreateMap<LectureSession, UserLecturesDto>()
                .ForMember(dest => dest.LectureSessionId, opt => opt.MapFrom(src => src.LectureSessionId))
                .ForMember(dest => dest.LectureCode, opt => opt.MapFrom(src => src.LectureCode))
                .ForMember(dest => dest.LectureName, opt => opt.MapFrom(src => src.Lecture.Name))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));


            CreateMap<Student, StudentDto>()
             .ForMember(dest => dest.Grade,
                        opt => opt.MapFrom(src => src.GradeLevel))
             .ForMember(dest => dest.IsManager,
                        opt => opt.MapFrom(src => src.ClubMemberships.Any(cm => cm.IsClubManager) ? 1 : 0))
             .ForMember(dest => dest.ClubId,
           opt => opt.MapFrom(src => src.ClubMemberships.Any(cm => cm.IsClubManager)
                                    ? src.ClubMemberships.First(cm => cm.IsClubManager).ClubId
                                    : 0))

             .ForMember(dest => dest.DepartmentName,
                        opt => opt.MapFrom(src => src.Department.Name))
             .ForMember(dest => dest.Schedule,
                        opt => opt.MapFrom(src =>
                            src.Enrollments.SelectMany(e => e.Lecture.LectureSessions)
                                .Select(ls => new UserLecturesDto
                                {
                                    LectureSessionId = ls.LectureSessionId,
                                    LectureCode = ls.Lecture.Code,
                                    LectureName = ls.Lecture.Name,
                                    RoomName = ls.Room.Name,
                                    StartTime = ls.StartTime,
                                    EndTime = ls.EndTime,
                                    Date = ls.Date
                                }).ToList()));

            CreateMap<Exam, ExamSessionPostDto>()
                .ForMember(dest => dest.LectureCode, opt => opt.MapFrom(src => src.LectureCode))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Lecture.Grade))
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Lecture.Enrollments.Count == 0 ? 50 : 45));



            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.ImagePaths));


            CreateMap<Instructor, InstructorDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src =>
                    src.LectureInstructors.SelectMany(e => e.Lecture.LectureSessions)));

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Club, ClubDto>()
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ClubLogo, opt => opt.MapFrom(src => src.ClubLogoPath))
                .ForMember(dest => dest.ClubShorcut, opt => opt.MapFrom(src => src.NameShortcut))
                .ForMember(dest => dest.ClubManager, opt => opt.MapFrom(src => GetClubManagerFullName(src.ClubMemberships)))
                .ForMember(dest => dest.ClubManagerId, opt => opt.MapFrom(src => GetClubManagerId(src.ClubMemberships)));



            CreateMap<ClubReservation, ClubReservationGetDto>()
             .ForMember(dest => dest.ReservationId,
                                opt => opt.MapFrom(src => src.ReservationId))
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




            CreateMap<Room, ExamRoomDto>().ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Exam, ExamListDto>().ForMember(dest => dest.DepartmentName,
                                             opt => opt.MapFrom(src => src.Lecture.Department.Name));




            CreateMap<LectureSession, LectureSessionDto>();




            CreateMap<Exam, ExamDto>();
            CreateMap<Enrollment, EnrollmentDto>();


            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, DepartmentForCreateDto>();
            CreateMap<Department, DepartmentForUpdateDto>();






            CreateMap<Building, BuildingDto>();
            CreateMap<Building, BuildingForCreateDto>();
            CreateMap<Building, BuildingForUpdateDto>();
            CreateMap<BuildingForCreateDto, Building>();
            CreateMap<BuildingForUpdateDto, Building>();






            CreateMap<Reservation, ReservationDto>();

            CreateMap<Notification, NotificationDto>();


            CreateMap<LectureSession, UserLecturesDto>()
                // LectureSession tablosunda LectureCode sütunu var, 
                // aynı zamanda Lecture.Name gibi bilgilere de erişebilirsiniz.
                .ForMember(dest => dest.LectureCode, opt => opt.MapFrom(src => src.LectureCode))
                .ForMember(dest => dest.LectureName, opt => opt.MapFrom(src => src.Lecture.Name))
                // Oda adını, Room tablosundan alırsınız (RoomName vb. property varsayarak)
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            // Student -> StudentLoginDto Mapleme
            CreateMap<Student, StudentLoginDto>()
            .ForMember(dest => dest.Id,
                       opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.Grade,
                       opt => opt.MapFrom(src => src.GradeLevel))
            .ForMember(dest => dest.DepartmentName,
                       opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.StudentClubs,
                       opt => opt.MapFrom(src => src.ClubMemberships.Select(cm => new StudentClubs
                       {
                           ClubId = cm.ClubId,
                           ClubName = cm.Club.Name,
                           ClubShorcut = cm.Club.NameShortcut,
                           IsManager = cm.IsClubManager
                       })))
            // Burada Enrollment -> Lecture -> LectureSessions ilişkisi var
            // Her Enrollment üzerinden LectureSessions'a ulaşıp 
            // tek bir listeye dönüştürmek için "SelectMany" genelde daha uygun olur:
            .ForMember(dest => dest.Schedule,
                       opt => opt.MapFrom(src => src.Enrollments
                           .SelectMany(e => e.Lecture.LectureSessions)));
        }
    }
}