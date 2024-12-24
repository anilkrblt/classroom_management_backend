using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClubService
    {
        // Get all clubs
        Task<IEnumerable<ClubDto>> GetAllClubsAsync(bool trackChanges);

        // Get a specific club by ID
        Task<ClubDto> GetClubByIdAsync(int clubId, bool trackChanges);

        // Get members of a specific club
        Task<IEnumerable<StudentDto>> GetClubMembersAsync(int clubId, bool trackChanges);

        // Update club manager (president)
        Task UpdateClubManagerAsync(int clubId, int studentId);

        // Create a new club
        Task CreateClubAsync(ClubDto clubDto);

        // Update an existing club
        Task UpdateClubAsync(int clubId, ClubDto clubDto);

        // Delete a club
        Task DeleteClubAsync(int clubId);
    }
}
