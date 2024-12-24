using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllExamsAsync(bool trackChanges);
        Task<Exam> GetExamAsync(int examId, bool trackChanges);

        void CreateExam(Exam exam);

        Task UpdateExamAsync(Exam exam);
        Task DeleteExamAsync(Exam exam);
    }
}
