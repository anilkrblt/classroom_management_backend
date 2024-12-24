using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(e => e.EnrollmentId) 
                .ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentAsync(int enrollmentId, bool trackChanges)
        {
            return await FindByCondition(e => e.EnrollmentId == enrollmentId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateEnrollment(Enrollment enrollment)
        {
            Create(enrollment);
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            var existingEnrollment = await GetEnrollmentAsync(enrollment.EnrollmentId, true);
            if (existingEnrollment != null)
            {
                existingEnrollment.StudentId = enrollment.StudentId;
                existingEnrollment.LectureCode = enrollment.LectureCode;

                Update(existingEnrollment);
            }
        }
        public async Task DeleteEnrollmentAsync(Enrollment enrollment)
        {
            var existingEnrollment = await GetEnrollmentAsync(enrollment.EnrollmentId, true);
            if (existingEnrollment != null)
            {
                Delete(existingEnrollment);
            }
        }
    }
}
