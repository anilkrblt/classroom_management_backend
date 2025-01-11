using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IExamSessionRepository
    {
        Task<IEnumerable<ExamSession>> GetAllExamSessionsAsync(bool trackChanges);
        Task<ExamSession> GetExamSessionAsync(int examSessionId, bool trackChanges);

        void CreateExamSession(ExamSession examSession);

        Task UpdateExamSessionAsync(ExamSession examSession);

        void DeleteAllExamSessions(List<ExamSession> examSessions);

        Task DeleteExamSessionAsync(ExamSession examSession);
    }
}
