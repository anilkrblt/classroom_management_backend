using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges);
        Task<Club> GetClubAsync(int clubId, bool trackChanges);
        Task<Club> GetClubByNameAsync(string clubName, bool trackChanges);

        void CreateClub(Club club);
        Task UpdateClubAsync(Club club);
        Task DeleteClubAsync(Club club);
    }
}
