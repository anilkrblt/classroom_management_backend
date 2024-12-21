using System;
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
            return await FindAll(trackChanges).OrderBy(e => e.Student.Name).ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentAsync(int enrollmentId, bool trackChanges)
        {
            return await FindByCondition(e => e.StudentId == enrollmentId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId, bool trackChanges)
        {
            return await FindByCondition(e => e.StudentId == studentId, trackChanges).ToListAsync();
        }

        public void DeleteEnrollment(Enrollment enrollment)
        {
            Delete(enrollment);
        }

        public void CreateEnrollment(Enrollment enrollment)
        {
            Create(enrollment);
        }
    }
}
