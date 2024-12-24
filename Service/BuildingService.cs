using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class BuildingService : IBuildingService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public BuildingService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get all buildings
        public async Task<IEnumerable<BuildingDto>> GetAllBuildingsAsync(bool trackChanges)
        {
            var buildings = await _repository.Building.GetAllBuildingsAsync(trackChanges);
            return _mapper.Map<IEnumerable<BuildingDto>>(buildings);
        }

        // Get a specific building by ID
        public async Task<BuildingDto> GetBuildingByIdAsync(int buildingId, bool trackChanges)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, trackChanges);

            if (building == null)
                throw new KeyNotFoundException($"Building with ID {buildingId} not found.");

            return _mapper.Map<BuildingDto>(building);
        }

        // Get rooms of a specific building
        public async Task<IEnumerable<RoomDto>> GetRoomsByBuildingIdAsync(int buildingId, bool trackChanges)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, trackChanges);

            if (building == null)
                throw new KeyNotFoundException($"Building with ID {buildingId} not found.");

            var rooms = await _repository.Room.GetRoomsByBuildingId(buildingId, trackChanges);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        // Create a new building
        public async Task CreateBuildingAsync(BuildingDto buildingDto)
        {
            var building = _mapper.Map<Building>(buildingDto);
            _repository.Building.CreateBuilding(building);
            await _repository.SaveAsync();
        }

        // Update an existing building
        public async Task UpdateBuildingAsync(int buildingId, BuildingDto buildingDto)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, true);

            if (building == null)
                throw new KeyNotFoundException($"Building with ID {buildingId} not found.");

            _mapper.Map(buildingDto, building);
            await _repository.SaveAsync();
        }

        // Delete a building
        public async Task DeleteBuildingAsync(int buildingId)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, true);

            if (building == null)
                throw new KeyNotFoundException($"Building with ID {buildingId} not found.");

            await _repository.Building.DeleteBuildingAsync(building);
            await _repository.SaveAsync();
        }
    }
}
