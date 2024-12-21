namespace Contracts
{
    public interface IRepositoryManager
    {

        IStudentRepository Student { get; }
        IBuildingRepository Building { get; }
        IClassRepository Class { get; }
        IClubRepository Club { get; }
        IClubReservationRepository ClubReservation { get; }
        IDepartmentRepository Department { get; }
        IEmployeeRepository Employee { get; }
        IEnrollmentRepository Enrollment { get; }
        IExamClassRepository ExamClass { get; }
        IExamRepository Exam { get; }
        IInstructorRepository Instructor { get; }
        ILabRepository Lab { get; }
        ILectureHallRepository LectureHall { get; }
        ILectureRepository Lecture { get; }
        ILectureReservationRepository LectureReservation { get; }
        ILectureSessionRepository LectureSession { get; }
        IRequestRepository Request { get; }
        IReservationRepository Reservation { get; }
        IRoomRepository Room { get; }
        Task SaveAsync();
    }
}
