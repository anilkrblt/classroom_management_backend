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
        private readonly Lazy<IClubMembershipRepository> _clubMembershipRepository;
        private readonly Lazy<IExamSessionRepository> _examSessionRepository;
        private readonly Lazy<INotificationRecipientRepository> _notificationRecipientRepository;
        private readonly Lazy<INotificationRepository> _notificationRepository;
        private readonly Lazy<IInstructorPreferenceRepository> _instructorPreferenceRepository;
        private readonly Lazy<ILectureRepository> _lectureRepository;
        private readonly Lazy<IInstructorRepository> _instructorRepository;
        private readonly Lazy<IExamRepository> _examRepository;
        private readonly Lazy<IEnrollmentRepository> _enrollmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IClubRepository> _clubRepository;
        private readonly Lazy<IBuildingRepository> _buildingRepository;



        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(repositoryContext));
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
            _requestRepository = new Lazy<IRequestRepository>(() => new RequestRepository(repositoryContext));
            _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(repositoryContext));
            _lectureSessionRepository = new Lazy<ILectureSessionRepository>(() => new LectureSessionRepository(repositoryContext));
            _lectureRepository = new Lazy<ILectureRepository>(() => new LectureRepository(repositoryContext));
            _instructorRepository = new Lazy<IInstructorRepository>(() => new InstructorRepository(repositoryContext));
            _examRepository = new Lazy<IExamRepository>(() => new ExamRepository(repositoryContext));
            _enrollmentRepository = new Lazy<IEnrollmentRepository>(() => new EnrollmentRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(repositoryContext));
            _clubRepository = new Lazy<IClubRepository>(() => new ClubRepository(repositoryContext));
            _buildingRepository = new Lazy<IBuildingRepository>(() => new BuildingRepository(repositoryContext));
            _clubMembershipRepository = new Lazy<IClubMembershipRepository>(() => new ClubMembershipRepository(repositoryContext));
            _examSessionRepository = new Lazy<IExamSessionRepository>(() => new ExamSessionRepository(repositoryContext));
            _notificationRecipientRepository = new Lazy<INotificationRecipientRepository>(() => new NotificationRecipientRepository(repositoryContext));
            _notificationRepository = new Lazy<INotificationRepository>(() => new NotificationRepository(repositoryContext));
            _instructorPreferenceRepository = new Lazy<IInstructorPreferenceRepository>(() => new InstructorPreferenceRepository(repositoryContext));


        }
        public IStudentRepository Student => _studentRepository.Value;
        public IBuildingRepository Building => _buildingRepository.Value;
        public IClubRepository Club => _clubRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IEnrollmentRepository Enrollment => _enrollmentRepository.Value;
        public IExamRepository Exam => _examRepository.Value;
        public IInstructorRepository Instructor => _instructorRepository.Value;
        public ILectureRepository Lecture => _lectureRepository.Value;
        public ILectureSessionRepository LectureSession => _lectureSessionRepository.Value;
        public IRequestRepository Request => _requestRepository.Value;
        public IReservationRepository Reservation => _reservationRepository.Value;
        public IRoomRepository Room => _roomRepository.Value;
        public IClubMembershipRepository ClubMembership => _clubMembershipRepository.Value;
        public IExamSessionRepository ExamSession => _examSessionRepository.Value;
        public IInstructorPreferenceRepository InstructorPreference => _instructorPreferenceRepository.Value;
        public INotificationRecipientRepository NotificationRecipient => _notificationRecipientRepository.Value;
        public INotificationRepository Notification => _notificationRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}