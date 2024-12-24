using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IInstructorPreferenceRepository
    {
        Task<IEnumerable<InstructorPreference>> GetAllInstructorPreferencesAsync(bool trackChanges);
        Task<InstructorPreference> GetInstructorPreferenceAsync(int preferenceId, bool trackChanges);

        void CreateInstructorPreference(InstructorPreference instructorPreference);

        Task UpdateInstructorPreferenceAsync(InstructorPreference instructorPreference);
        Task DeleteInstructorPreferenceAsync(InstructorPreference instructorPreference);
    }
}
