using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBuildingService
    {
        // Get all buildings
        Task<IEnumerable<BuildingDto>> GetAllBuildingsAsync(bool trackChanges);

        // Get a specific building by ID
        Task<BuildingDto> GetBuildingByIdAsync(int buildingId, bool trackChanges);

        // Get rooms of a specific building
        Task<IEnumerable<RoomDto>> GetRoomsByBuildingIdAsync(int buildingId, bool trackChanges);

        // Create a new building
        Task CreateBuildingAsync(BuildingForCreateDto buildingForCreateDto);

        // Update an existing building
        Task UpdateBuildingAsync(int buildingId, BuildingForUpdateDto buildingForUpdateDto);

        // Delete a building
        Task DeleteBuildingAsync(int buildingId);
    }
}
