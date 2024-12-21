

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IBuildingService BuildingService{get;}
        IClubService    ClubService{get;}
        IDepartmentService DepartmentService{get;}
        IEmployeeService EmployeeService{get;}
        IExamService   ExamService{get;}
        IInstructorService InstructorService{get;}
        ILectureService LectureService{get;}
        IStudentService StudentService { get; }
        IRoomService RoomService {get; }
        IReservationService ReservationService {get; }
        IRequestService RequestService {get; }

    }

}
