using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ExamRepository : RepositoryBase<Exam>, IExamRepository
    {
        public ExamRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Exam>> GetAllExamsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(e => e.Name) 
                .ToListAsync();
        }

        
        public async Task<Exam> GetExamAsync(int examId, bool trackChanges)
        {
            return await FindByCondition(e => e.ExamId == examId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateExam(Exam exam)
        {
            Create(exam);
        }

        public async Task UpdateExamAsync(Exam exam)
        {
            var existingExam = await GetExamAsync(exam.ExamId, true);
            if (existingExam != null)
            {
                existingExam.Name = exam.Name;
                existingExam.LectureCode = exam.LectureCode;

                Update(existingExam);
            }
        }

        public async Task DeleteExamAsync(Exam exam)
        {
            var existingExam = await GetExamAsync(exam.ExamId, true);
            if (existingExam != null)
            {
                Delete(existingExam);
            }
        }
    }
}
