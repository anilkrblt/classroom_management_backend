using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(r => r.Name) 
                .ToListAsync();
        }

        public async Task<Room> GetRoomAsync(int roomId, bool trackChanges)
        {
            return await FindByCondition(r => r.RoomId == roomId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(r => ids.Contains(r.RoomId), trackChanges).ToListAsync();
        }

        public void DeleteRoom(Room room)
        {
            Delete(room);
        }

        public void CreateRoomForBuilding(int buildingId, Room room)
        {
            room.BuildingId = buildingId; 
            Create(room);
        }
    }
}
