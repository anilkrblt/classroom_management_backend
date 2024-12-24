using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<Building>> GetAllBuildingsAsync(bool trackChanges);
        Task<Building> GetBuildingAsync(int buildingId, bool trackChanges);
        void CreateBuilding(Building building);
        Task UpdateBuildingAsync(Building building);
        Task DeleteBuildingAsync(Building building);
    }
}
