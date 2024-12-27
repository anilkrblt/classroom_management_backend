using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureInstructorRepository : RepositoryBase<LectureInstructor>, ILectureInstructor
    {
        public LectureInstructorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

     
        public async Task<IEnumerable<LectureInstructor>> GetAllInstructorLecturesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(li => li.Lecture)       
                .Include(li => li.Instructor)    
                .ToListAsync();
        }

      
        public async Task<IEnumerable<LectureInstructor>> GetInstructorLecturesAsync(int instructorId, bool trackChanges)
        {
            return await FindByCondition(li => li.InstructorId == instructorId, trackChanges)
                .Include(li => li.Lecture)
                .Include(li => li.Instructor)
                .ToListAsync();
        }


        public void CreateInstructorLecture(LectureInstructor lectureInstructor) => Create(lectureInstructor);


        public async Task UpdateInstructorLectureAsync(LectureInstructor lectureInstructor)
        {
            Update(lectureInstructor);
            await Task.CompletedTask;
        }


        public void DeleteInstructorLecture(LectureInstructor lectureInstructor) => Delete(lectureInstructor);
    }
}
