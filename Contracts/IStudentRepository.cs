using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);
        Task<Student> GetStudentAsync(int studentId, bool trackChanges);
        Task<IEnumerable<Student>> GetStudentsByDepartmentId(int departmentId, bool trackChanges);

        Student GetStudentByEmail(string email);


        void CreateStudent(Student student);

        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
    }
}
