using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IClubMembershipRepository
    {
        Task<IEnumerable<ClubMembership>> GetAllClubMembershipsAsync(bool trackChanges);
        Task<ClubMembership> GetClubMembershipAsync(int clubMembershipId, bool trackChanges);

        void CreateClubMembership(ClubMembership clubMembership);
        Task UpdateClubMembershipAsync(ClubMembership clubMembership);
        Task DeleteClubMembershipAsync(ClubMembership clubMembership);
    }
}
