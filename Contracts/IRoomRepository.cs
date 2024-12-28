using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges);
        Task<Room> GetRoomAsync(int roomId, bool trackChanges);
        Task<Room> GetRoomByNameAsync(string roomName, bool trackChanges);

        Task<IEnumerable<Room>> GetRoomsByDepartmentId(int departmentId, bool trackChanges);
        Task<IEnumerable<Room>> GetRoomsByBuildingId(int buildingId, bool trackChanges);
        void CreateRoom(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(Room room);
    }
}
