using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(bool trackChanges);
        Task<Enrollment> GetEnrollmentAsync(int enrollmentId, bool trackChanges);

        void CreateEnrollment(Enrollment enrollment);

        Task UpdateEnrollmentAsync(Enrollment enrollment);
        Task DeleteEnrollmentAsync(Enrollment enrollment);
    }
}
