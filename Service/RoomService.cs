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
        public async Task<RoomDto> CreateRoomAsync(RoomCreationForBuildingDto creationDto)
        {
            // 1) Map DTO -> Room entity
            var roomEntity = _mapper.Map<Room>(creationDto);

            // 2) Kaydet
            var room = await _repositoryManager.Room.CreateRoomAsync(roomEntity);
            await _repositoryManager.SaveAsync();

            // 3) Geriye dönmek için (CreatedAtAction) => RoomDto
            var createdRoomDto = _mapper.Map<RoomDto>(room);
            return createdRoomDto;
        }


        // Update an existing room
        public async Task UpdateRoomAsync(int roomId, RoomUpdateForBuildingDto updateDto)
        {
            // 1) Mevcut kaydı bul
            var room = await _repositoryManager.Room.GetRoomAsync(roomId, trackChanges: true);
            if (room == null)
                throw new KeyNotFoundException($"Room with ID {roomId} not found.");

            // 2) DTO -> Entity
            //    Burada basit alanları AutoMapper ile map’leyebilirsiniz. 
            //    (room.Name, room.Capacity, vb.)
            _mapper.Map(updateDto, room);

            // 3) Eğer "Equipment" null geldiyse, tablo NOT NULL constraint yüzünden 
            //    set etmezsek hata alırız. Aşağıda gösterilen AutoMapper profilindeki
            //    ".ForMember(... => Equipment, src => src.Equipment ?? string.Empty)" 
            //    satırı varsa burada ekstra kod yazmaya gerek yoktur.
            //    Alternatif olarak:
            if (room.Equipment == null)
                room.Equipment = string.Empty;

            // 4) Kaydet
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
