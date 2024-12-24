using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class InstructorPreferenceRepository : RepositoryBase<InstructorPreference>, IInstructorPreferenceRepository
    {
        public InstructorPreferenceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<InstructorPreference>> GetAllInstructorPreferencesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(ip => ip.PreferenceId) 
                .ToListAsync();
        }

        public async Task<InstructorPreference> GetInstructorPreferenceAsync(int preferenceId, bool trackChanges)
        {
            return await FindByCondition(ip => ip.PreferenceId == preferenceId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateInstructorPreference(InstructorPreference instructorPreference)
        {
            Create(instructorPreference);
        }

        public async Task UpdateInstructorPreferenceAsync(InstructorPreference instructorPreference)
        {
            var existingPreference = await GetInstructorPreferenceAsync(instructorPreference.PreferenceId, true);
            if (existingPreference != null)
            {
                existingPreference.LectureCode = instructorPreference.LectureCode;
                existingPreference.InstructorId = instructorPreference.InstructorId;
                existingPreference.PreferredTime = instructorPreference.PreferredTime;
                existingPreference.UnavailableTimes = instructorPreference.UnavailableTimes;

                Update(existingPreference);
            }
        }

        public async Task DeleteInstructorPreferenceAsync(InstructorPreference instructorPreference)
        {
            var existingPreference = await GetInstructorPreferenceAsync(instructorPreference.PreferenceId, true);
            if (existingPreference != null)
            {
                Delete(existingPreference);
            }
        }
    }
}
