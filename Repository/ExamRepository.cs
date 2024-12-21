using System;
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
                .OrderBy(e => e.ExamDate) 
                .ToListAsync();
        }

        public async Task<Exam> GetExamAsync(int examId, bool trackChanges)
        {
            return await FindByCondition(e => e.ExamId == examId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Exam>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(e => ids.Contains(e.ExamId), trackChanges)
                .ToListAsync();
        }

        public void DeleteExam(Exam exam)
        {
            Delete(exam);
        }

        public void CreateExamForLecture(Exam exam, string lectureCode)
        {
            exam.Code = lectureCode; 
            Create(exam);
        }
    }
}
