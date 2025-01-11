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

        // Get all rooms
        public async Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).Include(r => r.Building)
                                              .Include(r => r.Department)
                                              .Include(r => r.LectureSessions)
                                                .ThenInclude(ls => ls.Lecture)
                                                .ThenInclude(l => l.Department)
                                              .Include(r => r.LectureSessions)
                                                .ThenInclude(ls => ls.Instructor)
                                              .Include(r => r.Reservations)
                                                .ThenInclude(r => r.ClubReservations
                                                    .Where(cr => cr.Status.ToLower() == "approved"))
                                                    .ThenInclude(cr => cr.Club)
                                              .OrderBy(r => r.Name)
                                              .ToListAsync();
        }

        public async Task<Room> GetRoomAsync(int roomId, bool trackChanges)
        {
            return await FindByCondition(r => r.RoomId == roomId, trackChanges)
                                        .Include(r => r.Building)
                                        .Include(r => r.Department)
                                        .Include(r => r.LectureSessions)
                                            .ThenInclude(ls => ls.Lecture)
                                                .ThenInclude(l => l.Department)
                                        .Include(r => r.LectureSessions)
                                            .ThenInclude(ls => ls.Instructor)
                                        .Include(r => r.Reservations)
                                            .ThenInclude(r => r.ClubReservations
                                                .Where(cr => cr.Status.ToLower() == "approved"))
                                                .ThenInclude(cr => cr.Club)
                                        .SingleOrDefaultAsync();
        }

        // Get rooms by department ID
        public async Task<IEnumerable<Room>> GetRoomsByDepartmentId(int departmentId, bool trackChanges)
        {
            return await FindByCondition(r => r.DepartmentId == departmentId, trackChanges)
                                            .Include(r => r.Building)
                                            .Include(r => r.Department)
                                            .Include(r => r.LectureSessions)
                                                .ThenInclude(ls => ls.Lecture)
                                                    .ThenInclude(l => l.Department)
                                            .Include(r => r.LectureSessions)
                                                .ThenInclude(ls => ls.Instructor)
                                            .OrderBy(r => r.Name)
                                            .ToListAsync();
        }

        // Get rooms by building ID
        public async Task<IEnumerable<Room>> GetRoomsByBuildingId(int buildingId, bool trackChanges)
        {
            return await FindByCondition(r => r.BuildingId == buildingId, trackChanges)
                                            .Include(r => r.Building)
                                            .Include(r => r.Department)
                                            .Include(r => r.LectureSessions)
                                                .ThenInclude(ls => ls.Lecture)
                                                    .ThenInclude(l => l.Department)
                                            .Include(r => r.LectureSessions)
                                                .ThenInclude(ls => ls.Instructor)
                                            .Include(r => r.Reservations)
                                                .ThenInclude(r => r.ClubReservations)
                                            .OrderBy(r => r.Name)
                                            .ToListAsync();
        }

        /*    // Create a new room
            public void CreateRoom(Room room)
            {
                Create(room);
            }
    */
        public async Task<Room> CreateRoomAsync(Room room)
        {
            // Room nesnesini oluştur
            Create(room);

            // Veritabanına değişiklikleri kaydet
            await RepositoryContext.SaveChangesAsync();

            // Oluşturulan Room nesnesini geri döndür
            return await FindByCondition(r => r.RoomId == room.RoomId, false)
                                    .Include(r => r.Building)
                                    .Include(r => r.Department)
                                    .Include(r => r.LectureSessions)
                                        .ThenInclude(ls => ls.Lecture)
                                            .ThenInclude(l => l.Department)
                                    .Include(r => r.LectureSessions)
                                        .ThenInclude(ls => ls.Instructor).SingleOrDefaultAsync();

        }
        // Update an existing room
        public async Task UpdateRoomAsync(Room room)
        {
            var existingRoom = await GetRoomAsync(room.RoomId, true);
            if (existingRoom != null)
            {
                existingRoom.Name = room.Name;
                existingRoom.Capacity = room.Capacity;
                existingRoom.IsActive = room.IsActive;
                existingRoom.RoomType = room.RoomType;
                existingRoom.IsProjectorWorking = room.IsProjectorWorking;
                existingRoom.Equipment = room.Equipment;
                existingRoom.BuildingId = room.BuildingId;
                existingRoom.DepartmentId = room.DepartmentId;

                Update(existingRoom);
            }
        }

        // Delete a room
        public async Task DeleteRoomAsync(Room room)
        {
            var existingRoom = await GetRoomAsync(room.RoomId, true);
            if (existingRoom != null)
            {
                Delete(existingRoom);
            }
        }

        public async Task<Room> GetRoomByNameAsync(string roomName, bool trackChanges)
        {
            return await FindByCondition(r => r.Name == roomName, trackChanges).SingleOrDefaultAsync();
        }
    }
}
