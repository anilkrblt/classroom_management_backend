using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllExamsAsync(bool trackChanges);
        Task<Exam> GetExamAsync(int examId, bool trackChanges);
        Task<IEnumerable<Exam>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteExam(Exam Exam);

        void CreateExamForLecture(Exam Exam,string lectureCode);


    }
}