using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public RoomService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all rooms
        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync(bool trackChanges)
        {
            var rooms = await _repositoryManager.Room.GetAllRoomsAsync(trackChanges);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        // Get a specific room by ID
        public async Task<RoomDto> GetRoomByIdAsync(int roomId, bool trackChanges)
        {
            var room = await _repositoryManager.Room.GetRoomAsync(roomId, trackChanges);

            if (room == null)
                throw new KeyNotFoundException($"Room with ID {roomId} not found.");

            return _mapper.Map<RoomDto>(room);
        }

        // Get rooms by building ID
        public async Task<IEnumerable<RoomDto>> GetRoomsByBuildingIdAsync(int buildingId, bool trackChanges)
        {
            var rooms = await _repositoryManager.Room.GetRoomsByBuildingId(buildingId, trackChanges);

            if (!rooms.Any())
                throw new KeyNotFoundException($"No rooms found for Building with ID {buildingId}.");

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        // Get rooms by department ID
        public async Task<IEnumerable<RoomDto>> GetRoomsByDepartmentIdAsync(int departmentId, bool trackChanges)
        {
            var rooms = await _repositoryManager.Room.GetRoomsByDepartmentId(departmentId, trackChanges);

            if (!rooms.Any())
                throw new KeyNotFoundException($"No rooms found for Department with ID {departmentId}.");

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        // Create a new room
        public async Task CreateRoomAsync(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);

            _repositoryManager.Room.CreateRoom(room);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing room
        public async Task UpdateRoomAsync(int roomId, RoomDto roomDto)
        {
            var room = await _repositoryManager.Room.GetRoomAsync(roomId, trackChanges: true);

            if (room == null)
                throw new KeyNotFoundException($"Room with ID {roomId} not found.");

            _mapper.Map(roomDto, room);
            await _repositoryManager.SaveAsync();
        }

        // Delete a room
        public async Task DeleteRoomAsync(int roomId)
        {
            var room = await _repositoryManager.Room.GetRoomAsync(roomId, trackChanges: true);

            if (room == null)
                throw new KeyNotFoundException($"Room with ID {roomId} not found.");

            await _repositoryManager.Room.DeleteRoomAsync(room);
            await _repositoryManager.SaveAsync();
        }
    }
}
