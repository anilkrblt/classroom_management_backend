using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using NLog;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class BuildingService : IBuildingService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public BuildingService(IRepositoryManager repository,ILogger logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper=mapper;
        }

        public async Task<IEnumerable<BuildingDto>> GetAllBuildingsAsync(bool trackChanges)
        {
            var buildings=  await _repository.Building.GetAllBuildingsAsync(trackChanges);
            var buildingDto=_mapper.Map<IEnumerable<BuildingDto>>(buildings);
            return buildingDto;
        }

        public async Task<BuildingDto> GetBuildingByIdAsync(int buildingId, bool trackChanges)
        {
            var building=await GetBuildingAndCheckIfItExists(buildingId,trackChanges);
            var buildingDto=_mapper.Map<BuildingDto>(building);
            return buildingDto;
        }


        private async Task<Building> GetBuildingAndCheckIfItExists(int buildingId, bool trackChanges)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, trackChanges);
            if (building is null)
                throw new BuildingNotFoundException(buildingId);
            return building;
        }
    }
}