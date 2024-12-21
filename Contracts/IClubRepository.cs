using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges);
        Task<Club> GetClubAsync(int clubId, bool trackChanges);
        Task<IEnumerable<Club>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteClub(Club Club);
        void CreateClub(Club Club);


    }
}