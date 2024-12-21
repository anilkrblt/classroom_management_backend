using Entities.Models;

namespace Contracts
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync(bool trackChanges);
        Task<Instructor> GetInstructorAsync(int instructorId, bool trackChanges);
        Task<IEnumerable<Instructor>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteInstructor(Instructor Instructor);

        void CreateInstructorForDepartment(Instructor Instructor,int departmentId);


    }
}