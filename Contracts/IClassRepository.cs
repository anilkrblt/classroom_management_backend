using Entities.Models;

namespace Contracts
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllClasssAsync(bool trackChanges);
        Task<Class> GetClassAsync(int classId, bool trackChanges);
        Task<IEnumerable<Class>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        void DeleteClass(Class Class);

        void CreateClassForDeparment(Class Class,int departmentId);

    }
}