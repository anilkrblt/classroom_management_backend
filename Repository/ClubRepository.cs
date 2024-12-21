using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ClubRepository : RepositoryBase<Club>, IClubRepository
    {
        public ClubRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Name) 
                .ToListAsync();
        }

        public async Task<Club> GetClubAsync(int clubId, bool trackChanges)
        {
            return await FindByCondition(c => c.ClubId == clubId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Club>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(c => ids.Contains(c.ClubId), trackChanges)
                .ToListAsync();
        }

        public void DeleteClub(Club club)
        {
            Delete(club);
        }

        public void CreateClub(Club club)
        {
            Create(club);
        }
    }
}
