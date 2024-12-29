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
                .Include(c => c.ClubMemberships)
                .ThenInclude(m => m.Student) // Öğrenci bilgilerini dahil etmek için
                .OrderBy(c => c.Name)
                .ToListAsync();
        }


        public async Task<Club> GetClubAsync(int clubId, bool trackChanges)
        {
            return await FindByCondition(c => c.ClubId == clubId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateClub(Club club)
        {
            Create(club);
        }

        public async Task UpdateClubAsync(Club club)
        {
            var existingClub = await GetClubAsync(club.ClubId, true);
            if (existingClub != null)
            {
                existingClub.Name = club.Name;

                Update(existingClub);
            }
        }

        public async Task DeleteClubAsync(Club club)
        {
            var existingClub = await GetClubAsync(club.ClubId, true);
            if (existingClub != null)
            {
                Delete(existingClub);
            }
        }

        public async Task<Club> GetClubByNameAsync(string clubName, bool trackChanges)
        {
            return await FindByCondition(c => c.Name == clubName, trackChanges).SingleOrDefaultAsync();
        }


    }
}
