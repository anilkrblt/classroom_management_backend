using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using NLog;
using Entities.Exceptions;

namespace Service
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public RoomService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync(bool trackChanges)
        {
            var rooms = await _repository.Room.GetAllRoomsAsync(trackChanges);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetRoomByIdAsync(int roomId, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room == null)
                throw new RoomNotFoundException(roomId);

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByDepartmentId(int departmentId, bool trackChanges)
        {

            var lhs = await _repository.LectureHall.GetAllLectureHallsAsync(trackChanges);
            var classes = await _repository.Class.GetAllClasssAsync(trackChanges);
            var labs = await _repository.Lab.GetAllLabsAsync(trackChanges);

            var lectureHallsByDepartment = lhs.Where(lh => lh.DepartmentId == departmentId).Select(lh => lh.Room).ToList();
            var classesByDepartment = classes.Where(c => c.DepartmentId == departmentId).Select(c => c.Room).ToList();
            var labsByDepartment = labs.Where(l => l.DepartmentId == departmentId).Select(l => l.Room).ToList();
            var rooms = lectureHallsByDepartment.Concat(labsByDepartment).Concat(classesByDepartment);


            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task DeleteRoomForBuildingAsync(int roomId, int buildingId, bool trackChanges)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, trackChanges);
            if (building == null)
                throw new ArgumentException("aaaa");

            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room == null)
                throw new RoomNotFoundException(roomId);
            _repository.Room.DeleteRoom(room);
            await _repository.SaveAsync();
        }

        public async Task UpdateRoomForBuilding(int buildingId, int roomId, RoomDto roomForUpdate, bool buildingTrackChanges, bool roomTrackChanges)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, buildingTrackChanges);
            if (building == null)
                throw new ArgumentException("aaaa");

            var room = await _repository.Room.GetRoomAsync(roomId, roomTrackChanges);
            if (room == null)
                throw new RoomNotFoundException(roomId);

            _mapper.Map(roomForUpdate, room);

            await _repository.SaveAsync();
        }

        public async Task<RoomDto> CreateRoomForBuilding(int buildingId, RoomDto roomForCreation, bool trackChanges)
        {
            var building = await _repository.Building.GetBuildingAsync(buildingId, trackChanges);
            if (building == null)
                throw new ArgumentException("aaaa");

            var roomEntity = _mapper.Map<Room>(roomForCreation);
            roomEntity.BuildingId = buildingId;

            _repository.Room.CreateRoomForBuilding(buildingId, roomEntity);
            await _repository.SaveAsync();

            return _mapper.Map<RoomDto>(roomEntity);
        }

        public async Task<IEnumerable<LectureHallDto>> GetAllLectureHallsAsync(bool trackChanges)
        {
            var lectureHalls = await _repository.LectureHall.GetAllLectureHallsAsync(trackChanges);
            return _mapper.Map<IEnumerable<LectureHallDto>>(lectureHalls);
        }

        public async Task<LectureHallDto> GetLectureHallByIdAsync(int roomId, bool trackChanges)
        {
            var lectureHall = await _repository.LectureHall.GetLectureHallAsync(roomId, trackChanges);
            if (lectureHall == null)
                throw new ArgumentException("Lecture Hall not found.");

            return _mapper.Map<LectureHallDto>(lectureHall);
        }

        public Task<LectureHallDto> CreateLectureHallForRoom(int roomId, LectureHallDto lectureHallForCreation, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LabDto>> GetAllLabsAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLabForRoom(int roomId, LabDto labForUpdate, bool labTrackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<LabDto> CreateLabForRoom(int roomId, LabDto labForCreation, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClassDto>> GetAllClassesAsync(bool trackChanges)
        {
            var classes = await _repository.Class.GetAllClasssAsync(trackChanges);
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }



        public Task UpdateClassForRoom(int roomId, ClassDto classForUpdate, bool classTrackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<ClassDto> CreateClassForRoom(int roomId, ClassDto classForCreation, bool trackChanges)
        {
            throw new NotImplementedException();
        }

    }
}
