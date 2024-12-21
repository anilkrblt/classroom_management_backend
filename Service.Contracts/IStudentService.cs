using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IStudentService
    {


        IEnumerable<StudentDto> GetAllStudent(bool trackChanges);
        StudentDto GetStudentById(int studentId, bool trackChanges);
        IEnumerable<StudentDto> GetStudentsByDepartmentId(int departmentId, bool trackChanges);
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync(bool trackChanges);
        Task<IEnumerable<LectureDto>> GetStudentLecturesByStudentIdAsync(int studentId, bool trackChanges);

    }
}
