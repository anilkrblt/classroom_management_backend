using Entities.Models;

namespace Contracts
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges);
        Task<Room> GetRoomAsync(int roomId, bool trackChanges);
        Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteRoom(Room Room);
        void CreateRoomForBuilding(int buildingId, Room Room);


    }
}