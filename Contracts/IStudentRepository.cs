using Entities.Models;

namespace Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);
        Task<Student> GetStudentAsync(int studentId, bool trackChanges);
        Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        
        void DeleteStudent(Student Student);

        void CreateStudentForDepartment(int departmentId, Student student);

        Task<IEnumerable<Student>> GetStudentsByDepartmentId(int departmentId, bool trackChanges);

    }
}
