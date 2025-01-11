using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ExamSessionRepository : RepositoryBase<ExamSession>, IExamSessionRepository
    {
        public ExamSessionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ExamSession>> GetAllExamSessionsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                        .Include(es => es.Exam)
                            .ThenInclude(e => e.Lecture)
                        .OrderBy(es => es.ExamDate)
                        .ToListAsync();
        }

        public async Task<ExamSession> GetExamSessionAsync(int examSessionId, bool trackChanges)
        {
            return await FindByCondition(es => es.ExamSessionId == examSessionId, trackChanges).SingleOrDefaultAsync();
        }

        public void CreateExamSession(ExamSession examSession)
        {
            Create(examSession);
        }

        public async Task UpdateExamSessionAsync(ExamSession examSession)
        {
            var existingExamSession = await GetExamSessionAsync(examSession.ExamSessionId, true);
            if (existingExamSession != null)
            {
                existingExamSession.ExamDate = examSession.ExamDate;
                existingExamSession.StartTime = examSession.StartTime;
                existingExamSession.EndTime = examSession.EndTime;
                existingExamSession.RoomId = examSession.RoomId;
                existingExamSession.ExamId = examSession.ExamId;

                Update(existingExamSession);
            }
        }

        public async Task DeleteExamSessionAsync(ExamSession examSession)
        {
            var existingExamSession = await GetExamSessionAsync(examSession.ExamSessionId, true);
            if (existingExamSession != null)
            {
                Delete(existingExamSession);
            }
        }

        public void DeleteAllExamSessions(List<ExamSession> examSessions)
        {
            foreach (var exam in examSessions)
            {

                Delete(exam);

            }
            RepositoryContext.SaveChanges();
        }
    }
}
