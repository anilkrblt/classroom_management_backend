using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NLog;
using Service;
using Service.Contracts;
using System;

namespace Service
{


    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<IBuildingService> _buildingService;
        private readonly Lazy<IClubService> _clubService;
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IExamService> _examService;
        private readonly Lazy<IInstructorService> _instructorService;
        private readonly Lazy<ILectureService> _lectureService;
        private readonly Lazy<ILectureSessionService> _lectureSessionService;
        private readonly Lazy<IRoomService> _roomService;
        private readonly Lazy<IReservationService> _reservationService;
        private readonly Lazy<IRequestService> _requestService;
        private readonly Lazy<INotificationService> _notificationService;
        private readonly Lazy<IAuthService> _authService;


        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper,
                              UserManager<User> userManager, IConfiguration configuration)
        {
            _studentService = new Lazy<IStudentService>(() =>
                new StudentService(repositoryManager, mapper));

            _buildingService = new Lazy<IBuildingService>(() =>
                new BuildingService(repositoryManager, mapper));

            _clubService = new Lazy<IClubService>(() =>
                new ClubService(repositoryManager, mapper));

            _departmentService = new Lazy<IDepartmentService>(() =>
                new DepartmentService(repositoryManager, mapper));

            _employeeService = new Lazy<IEmployeeService>(() =>
                new EmployeeService(repositoryManager, mapper));

            _examService = new Lazy<IExamService>(() =>
                new ExamService(repositoryManager, mapper));

            _instructorService = new Lazy<IInstructorService>(() =>
                new InstructorService(repositoryManager, mapper));

            _lectureService = new Lazy<ILectureService>(() =>
                new LectureService(repositoryManager, mapper));

            _lectureSessionService = new Lazy<ILectureSessionService>(() =>
                new LectureSessionService(repositoryManager, mapper));

            _roomService = new Lazy<IRoomService>(() =>
                new RoomService(repositoryManager, mapper));

            _reservationService = new Lazy<IReservationService>(() =>
                new ReservationService(repositoryManager, mapper));

            _requestService = new Lazy<IRequestService>(() =>
                new RequestService(repositoryManager, mapper));

            _notificationService = new Lazy<INotificationService>(() =>
                new NotificationService(repositoryManager, mapper));
            _authService = new Lazy<IAuthService>(() =>
                new AuthService(userManager,configuration, repositoryManager, mapper));

        }

        public IStudentService StudentService => _studentService.Value;
        public IBuildingService BuildingService => _buildingService.Value;
        public IClubService ClubService => _clubService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IExamService ExamService => _examService.Value;
        public IInstructorService InstructorService => _instructorService.Value;
        public ILectureService LectureService => _lectureService.Value;
        public ILectureSessionService LectureSessionService => _lectureSessionService.Value;
        public IRoomService RoomService => _roomService.Value;
        public IReservationService ReservationService => _reservationService.Value;
        public IRequestService RequestService => _requestService.Value;
        public INotificationService NotificationService => _notificationService.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
