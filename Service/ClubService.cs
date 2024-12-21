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
    public class ClubService : IClubService
    {


        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


        public ClubService(IRepositoryManager repository,ILogger logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper=mapper;
        }

        public async Task<ClubDto> CreateClub(ClubDto clubForCreation, bool trackChanges)
        {
            var clubEntity=_mapper.Map<Club>(clubForCreation);
            _repository.Club.CreateClub(clubEntity);
            await _repository.SaveAsync();
            var clubToReturn=_mapper.Map<ClubDto>(clubEntity);
            return clubToReturn;
        }

        public async Task DeleteClubAsync(int clubId, bool trackChanges)
        {
            var club=await GetClubAndCheckIfItExists(clubId,trackChanges);
            _repository.Club.DeleteClub(club);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<ClubDto>> GetAllClubsAsync(bool trackChanges)
        {
            var clubs=await _repository.Club.GetAllClubsAsync(trackChanges);
            var clubsDto=_mapper.Map<IEnumerable<ClubDto>>(clubs);
            return clubsDto;
        }

        public async Task<ClubDto> GetClubByIdAsync(int clubId, bool trackChanges)
        {
            var club=await GetClubAndCheckIfItExists(clubId,trackChanges);
            var clubDto=_mapper.Map<ClubDto>(club);
            return clubDto;
        }

        public async Task<IEnumerable<ClubDto>> GetClubsByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var clubEntities=await _repository.Club.GetByIdsAsync(ids,trackChanges);
            if (ids.Count() != clubEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var clubsToReturn = _mapper.Map<IEnumerable<ClubDto>>(clubEntities);

            return clubsToReturn;


        }

        public async Task UpdateClubAsync(int clubId, ClubDto clubForUpdate, bool trackChanges)
        {
            var club=await GetClubAndCheckIfItExists(clubId,trackChanges);
            _mapper.Map(clubForUpdate, club);
            await _repository.SaveAsync();

        }

        private async Task<Club> GetClubAndCheckIfItExists(int clubId, bool trackChanges)
        {
            var club = await _repository.Club.GetClubAsync(clubId, trackChanges);
            if (club is null)
                throw new ClubNotFoundException(clubId);
            return club;
        }
    }
}