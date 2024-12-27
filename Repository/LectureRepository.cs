using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureRepository : RepositoryBase<Lecture>, ILectureRepository
    {
        public LectureRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Lecture>> GetAllLecturesAsync(bool trackChanges)
        {
            var query = FindAll(trackChanges)
                .Include(l => l.LectureInstructors)
                    .ThenInclude(li => li.Instructor)
                .Include(l => l.Department)
                .OrderBy(l => l.Name);

            if (!trackChanges)
                query = (IOrderedQueryable<Lecture>)query.AsNoTracking();

            return await query.ToListAsync();
        }


        public async Task<Lecture> GetLectureAsync(string lectureCode, bool trackChanges)
        {
            return await FindByCondition(l => l.Code == lectureCode, trackChanges)
                 .Include(l => l.LectureInstructors)
                    .ThenInclude(li => li.Instructor)
                .Include(l => l.Department)
                .SingleOrDefaultAsync();
        }

        public void CreateLecture(Lecture lecture)
        {
            Create(lecture);
        }

        public async Task UpdateLectureAsync(Lecture lecture, string code)
        {
            var existingLecture = await GetLectureAsync(code, true);
            if (existingLecture != null)
            {
                existingLecture.Name = lecture.Name;
                existingLecture.DepartmentId = lecture.DepartmentId;

                Update(existingLecture);
            }
        }

        public async Task DeleteLectureAsync(Lecture lecture)
        {
            var existingLecture = await GetLectureAsync(lecture.Code, true);
            if (existingLecture != null)
            {
                Delete(existingLecture);
            }
        }

        public async Task<IEnumerable<Lecture>> GetLecturesByInstructorIdAsync(int instructorId, bool trackChanges)
        {
            return await FindByCondition(lecture => lecture.LectureSessions.Any(ls => ls.InstructorId == instructorId), trackChanges)
                .Include(lecture => lecture.Department)
                .Include(lecture => lecture.LectureSessions)
                .ToListAsync();
        }

    }
}
