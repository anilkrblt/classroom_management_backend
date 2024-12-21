using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DataTransferObjects;



namespace Service.Contracts
{
    public interface IClubService
    {
        Task<IEnumerable<ClubDto>> GetAllClubsAsync(bool trackChanges);
        Task<ClubDto> GetClubByIdAsync(int clubId, bool trackChanges);
        Task<IEnumerable<ClubDto>> GetClubsByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        Task DeleteClubAsync(int clubId, bool trackChanges);

        Task UpdateClubAsync(int clubId, ClubDto clubForUpdate, bool trackChanges);


        Task<ClubDto> CreateClub(ClubDto clubForCreation, bool trackChanges);
    }
}