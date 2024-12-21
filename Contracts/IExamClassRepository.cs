using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IExamClassRepository
    {
        Task<IEnumerable<ExamClass>> GetAllExamClasssAsync(bool trackChanges);
        Task<ExamClass> GetExamClassAsync(int examClassId, bool trackChanges);
        Task<IEnumerable<ExamClass>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteExamClass(ExamClass ExamClass);

        void CreateExamClass(ExamClass ExamClass);


    }
}