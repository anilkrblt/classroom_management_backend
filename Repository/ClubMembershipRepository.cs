using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ClubMembershipRepository : RepositoryBase<ClubMembership>, IClubMembershipRepository
    {
        public ClubMembershipRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ClubMembership>> GetAllClubMembershipsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(cm => cm.ClubMembershipId) 
                .ToListAsync();
        }

        public async Task<ClubMembership> GetClubMembershipAsync(int clubMembershipId, bool trackChanges)
        {
            return await FindByCondition(cm => cm.ClubMembershipId == clubMembershipId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateClubMembership(ClubMembership clubMembership)
        {
            Create(clubMembership);
        }

        public async Task UpdateClubMembershipAsync(ClubMembership clubMembership)
        {
            var existingMembership = await GetClubMembershipAsync(clubMembership.ClubMembershipId, true);
            if (existingMembership != null)
            {
                existingMembership.ClubId = clubMembership.ClubId;
                existingMembership.StudentId = clubMembership.StudentId;

                Update(existingMembership);
            }
        }

        public async Task DeleteClubMembershipAsync(ClubMembership clubMembership)
        {
            var existingMembership = await GetClubMembershipAsync(clubMembership.ClubMembershipId, true);
            if (existingMembership != null)
            {
                Delete(existingMembership);
            }
        }
    }
}
