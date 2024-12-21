using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LabRepository : RepositoryBase<Lab>, ILabRepository
    {
        public LabRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Lab>> GetAllLabsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(l => l.Room.Name) // Laboratuvarları isme göre sıralar
                .ToListAsync();
        }

        public async Task<Lab> GetLabAsync(int labId, bool trackChanges)
        {
            return await FindByCondition(l => l.RoomId == labId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Lab>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(l => ids.Contains(l.RoomId), trackChanges)
                .ToListAsync();
        }

        public void DeleteLab(Lab lab)
        {
            Delete(lab);
        }

        public void CreateLabForDepartment(Lab lab)
        {
            Create(lab);
        }
    }
}
