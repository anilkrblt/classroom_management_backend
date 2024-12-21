using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ExamClassRepository : RepositoryBase<ExamClass>, IExamClassRepository
    {
        public ExamClassRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ExamClass>> GetAllExamClasssAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(ec => ec.Room.Name)
                .ToListAsync();
        }

        public async Task<ExamClass> GetExamClassAsync(int examClassId, bool trackChanges)
        {
            return await FindByCondition(ec => ec.RoomId == examClassId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ExamClass>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(ec => ids.Contains(ec.RoomId), trackChanges).ToListAsync();
        }

        public void DeleteExamClass(ExamClass examClass)
        {
            Delete(examClass);
        }

        public void CreateExamClass(ExamClass examClass)
        {
            Create(examClass);
        }
    }
}
