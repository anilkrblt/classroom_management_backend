using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IRoomService
    {
        // Get all rooms
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync(bool trackChanges);

        // Get a specific room by ID
        Task<RoomDto> GetRoomByIdAsync(int roomId, bool trackChanges);

        // Get rooms by building ID
        Task<IEnumerable<RoomDto>> GetRoomsByBuildingIdAsync(int buildingId, bool trackChanges);

        // Get rooms by department ID
        Task<IEnumerable<RoomDto>> GetRoomsByDepartmentIdAsync(int departmentId, bool trackChanges);

        // Create a new room
        Task<RoomDto> CreateRoomAsync(RoomCreationForBuildingDto creationDto);

        // Update an existing room
        Task UpdateRoomAsync(int roomId, RoomUpdateForBuildingDto updateDto);

        // Delete a room
        Task DeleteRoomAsync(int roomId);
    }
}
