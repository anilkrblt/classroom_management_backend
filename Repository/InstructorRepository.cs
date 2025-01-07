using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class InstructorRepository : RepositoryBase<Instructor>, IInstructorRepository
    {
        public InstructorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(i => i.Department)
                .Include(i => i.LectureInstructors)
                    .ThenInclude(li => li.Lecture)
                        .ThenInclude(l => l.LectureSessions)
                            .ThenInclude(ls => ls.Room)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<Instructor> GetInstructorAsync(int instructorId, bool trackChanges)
        {
            return await FindByCondition(i => i.InstructorId == instructorId, trackChanges)
                   .Include(i => i.Department)
                   .Include(i => i.LectureInstructors)
                    .ThenInclude(li => li.Lecture)
                        .ThenInclude(l => l.LectureSessions)
                            .ThenInclude(ls => ls.Room)
                   .SingleOrDefaultAsync();
        }

        public void CreateInstructor(Instructor instructor)
        {
            Create(instructor);
        }
        public Instructor GetInstructorByEmail(string email)
        {
            var instructor = FindByCondition(i => i.Email == email, false).Include(i => i.Department).SingleOrDefault();
            if (instructor is null)
                return null;
            return instructor;

        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            var existingInstructor = await GetInstructorAsync(instructor.InstructorId, true);
            if (existingInstructor != null)
            {
                existingInstructor.Name = instructor.Name;
                existingInstructor.Email = instructor.Email;
                existingInstructor.Title = instructor.Title;
                existingInstructor.IsAdmin = instructor.IsAdmin;
                existingInstructor.DepartmentId = instructor.DepartmentId;

                Update(existingInstructor);
            }
        }

        public async Task DeleteInstructorAsync(Instructor instructor)
        {
            var existingInstructor = await GetInstructorAsync(instructor.InstructorId, true);
            if (existingInstructor != null)
            {
                Delete(existingInstructor);
            }
        }
    }
}
