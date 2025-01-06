using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync(bool trackChanges);
        Task<Instructor> GetInstructorAsync(int instructorId, bool trackChanges);

        void CreateInstructor(Instructor instructor);
        


        Instructor GetInstructorByEmail(string email);


        Task UpdateInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(Instructor instructor);
    }
}
