using AutoMapper;
using Contracts;
using Entities.Models;
using NLog;
using Service;
using Service.Contracts;

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
    private readonly Lazy<IRoomService> _roomService;
    private readonly Lazy<IReservationService> _reservationService;
    private readonly Lazy<IRequestService> _requestService;


    public ServiceManager(IRepositoryManager repositoryManager, ILogger logger, IMapper mapper)
    {



        _studentService = new Lazy<IStudentService>(() =>
                        new StudentService(repositoryManager, mapper, logger));


        _buildingService = new Lazy<IBuildingService>(() =>
                        new BuildingService(repositoryManager, logger, mapper));

        _clubService = new Lazy<IClubService>(() =>
                        new ClubService(repositoryManager, logger, mapper));


        _departmentService = new Lazy<IDepartmentService>(() =>
                        new DepartmentService(repositoryManager, logger, mapper));

        _employeeService = new Lazy<IEmployeeService>(() =>
                        new EmployeeService(repositoryManager, logger, mapper));


        _examService = new Lazy<IExamService>(() =>
                        new ExamService(repositoryManager, logger, mapper));

        _instructorService = new Lazy<IInstructorService>(() =>
                        new InstructorService(repositoryManager, logger, mapper));

        _lectureService = new Lazy<ILectureService>(() =>
                        new LectureService(repositoryManager, mapper, logger));

        _roomService = new Lazy<IRoomService>(() =>
                        new RoomService(repositoryManager, mapper, logger));

        _reservationService = new Lazy<IReservationService>(() =>
                        new ReservationService(repositoryManager, mapper, logger));

        _requestService = new Lazy<IRequestService>(() =>
                        new RequestService(repositoryManager, mapper, logger));
    }


    public IStudentService StudentService => _studentService.Value;
    public IBuildingService BuildingService => _buildingService.Value;
    public IClubService ClubService => _clubService.Value;
    public IDepartmentService DepartmentService => _departmentService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
    public IExamService ExamService => _examService.Value;
    public IInstructorService InstructorService => _instructorService.Value;
    public ILectureService LectureService => _lectureService.Value;
    public IRoomService RoomService => _roomService.Value;
    public IReservationService ReservationService => _reservationService.Value;
    public IRequestService RequestService => _requestService.Value;
}