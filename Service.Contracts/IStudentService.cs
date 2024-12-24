using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IStudentService
    {
        // Get all students
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges);

        // Get a specific student by ID
        Task<StudentDto> GetStudentByIdAsync(int studentId, bool trackChanges);

        // Get lectures for a specific student
        Task<IEnumerable<LectureDto>> GetStudentLecturesAsync(int studentId, bool trackChanges);

        // Get clubs for a specific student
        Task<IEnumerable<ClubDto>> GetStudentClubsAsync(int studentId, bool trackChanges);

        // Get exams for a specific student
        Task<IEnumerable<ExamDto>> GetStudentExamsAsync(int studentId, bool trackChanges);

        // Get students by department ID
        Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId, bool trackChanges);

        // Create a new student
        Task CreateStudentAsync(StudentDto studentDto);

        // Update an existing student
        Task UpdateStudentAsync(int studentId, StudentDto studentDto);

        // Delete a student
        Task DeleteStudentAsync(int studentId);
    }
}
    