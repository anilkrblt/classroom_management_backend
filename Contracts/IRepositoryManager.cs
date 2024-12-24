namespace Contracts
{
    public interface IRepositoryManager
    {

        IStudentRepository Student { get; }
        IBuildingRepository Building { get; }
        IClubRepository Club { get; }
        IClubMembershipRepository ClubMembership { get; }
        IDepartmentRepository Department { get; }
        IEmployeeRepository Employee { get; }
        IEnrollmentRepository Enrollment { get; }
        IExamRepository Exam { get; }
        IExamSessionRepository ExamSession { get; }
        IInstructorPreferenceRepository InstructorPreference { get; }
        IInstructorRepository Instructor { get; }
        ILectureRepository Lecture { get; }
        ILectureSessionRepository LectureSession { get; }
        INotificationRecipientRepository NotificationRecipient { get; }
        INotificationRepository Notification { get; }
        IRequestRepository Request { get; }
        IReservationRepository Reservation { get; }
        IRoomRepository Room { get; }
        Task SaveAsync();
    }
}
