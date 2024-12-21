using Contracts;
using Entities.Models;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IStudentRepository> _studentRepository;
        private readonly Lazy<IRoomRepository> _roomRepository;
        private readonly Lazy<IReservationRepository> _reservationRepository;
        private readonly Lazy<IRequestRepository> _requestRepository;
        private readonly Lazy<ILectureSessionRepository> _lectureSessionRepository;
        private readonly Lazy<ILectureReservationRepository> _lectureReservationRepository;
        private readonly Lazy<ILectureRepository> _lectureRepository;
        private readonly Lazy<ILectureHallRepository> _lectureHallRepository;
        private readonly Lazy<ILabRepository> _labRepository;
        private readonly Lazy<IInstructorRepository> _instructorRepository;
        private readonly Lazy<IExamRepository> _examRepository;
        private readonly Lazy<IExamClassRepository> _examClassRepository;
        private readonly Lazy<IEnrollmentRepository> _enrollmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IClubReservationRepository> _clubReservationRepository;
        private readonly Lazy<IClubRepository> _clubRepository;
        private readonly Lazy<IClassRepository> _classRepository;
        private readonly Lazy<IBuildingRepository> _buildingRepository;



        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(repositoryContext));
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
            _requestRepository = new Lazy<IRequestRepository>(() => new RequestRepository(repositoryContext));
            _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(repositoryContext));
            _lectureSessionRepository = new Lazy<ILectureSessionRepository>(() => new LectureSessionRepository(repositoryContext));
            _lectureReservationRepository = new Lazy<ILectureReservationRepository>(() => new LectureReservationRepository(repositoryContext));
            _lectureRepository = new Lazy<ILectureRepository>(() => new LectureRepository(repositoryContext));
            _lectureHallRepository = new Lazy<ILectureHallRepository>(() => new LectureHallRepository(repositoryContext));
            _labRepository = new Lazy<ILabRepository>(() => new LabRepository(repositoryContext));
            _instructorRepository = new Lazy<IInstructorRepository>(() => new InstructorRepository(repositoryContext));
            _examRepository = new Lazy<IExamRepository>(() => new ExamRepository(repositoryContext));
            _examClassRepository = new Lazy<IExamClassRepository>(() => new ExamClassRepository(repositoryContext));
            _enrollmentRepository = new Lazy<IEnrollmentRepository>(() => new EnrollmentRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(repositoryContext));
            _clubReservationRepository = new Lazy<IClubReservationRepository>(() => new ClubReservationRepository(repositoryContext));
            _clubRepository = new Lazy<IClubRepository>(() => new ClubRepository(repositoryContext));
            _classRepository = new Lazy<IClassRepository>(() => new ClassRepository(repositoryContext));
            _buildingRepository = new Lazy<IBuildingRepository>(() => new BuildingRepository(repositoryContext));
        }
        public IStudentRepository Student => _studentRepository.Value;
        public IBuildingRepository Building => _buildingRepository.Value;
        public IClassRepository Class => _classRepository.Value;
        public IClubRepository Club => _clubRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IEnrollmentRepository Enrollment => _enrollmentRepository.Value;
        public IExamClassRepository ExamClass => _examClassRepository.Value;
        public IExamRepository Exam => _examRepository.Value;
        public IInstructorRepository Instructor => _instructorRepository.Value;
        public ILabRepository Lab => _labRepository.Value;
        public ILectureHallRepository LectureHall => _lectureHallRepository.Value;
        public ILectureRepository Lecture => _lectureRepository.Value;
        public ILectureReservationRepository LectureReservation => _lectureReservationRepository.Value;
        public ILectureSessionRepository LectureSession => _lectureSessionRepository.Value;
        public IRequestRepository Request => _requestRepository.Value;
        public IReservationRepository Reservation => _reservationRepository.Value;
        public IRoomRepository Room => _roomRepository.Value;
        public IClubReservationRepository ClubReservation => _clubReservationRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}