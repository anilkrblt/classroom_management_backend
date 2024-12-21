using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(bool trackChanges);
        Task<Enrollment> GetEnrollmentAsync(int enrollmentId, bool trackChanges);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId, bool trackChanges);
        void DeleteEnrollment(Enrollment Enrollment);

        void CreateEnrollment(Enrollment Enrollment);


    }
}