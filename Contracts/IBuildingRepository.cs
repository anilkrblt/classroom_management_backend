using Entities.Models;

namespace Contracts
{
    public interface IBuildingRepository
    {

        Task<IEnumerable<Building>> GetAllBuildingsAsync(bool trackChanges);

        Task<Building> GetBuildingAsync(int buildingyId, bool trackChanges);

    }
}