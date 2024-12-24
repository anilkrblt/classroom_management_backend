using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
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
            CreateMap<Room, RoomDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<Request, RequestDto>();
            CreateMap<LectureSession, LectureSessionDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Exam, ExamDto>();
            CreateMap<Lecture, LectureDto>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Club, ClubDto>();
            CreateMap<Building, BuildingDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<Reservation, ReservationDto>();

        }
    }
}