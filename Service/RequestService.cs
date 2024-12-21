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
    public class RequestService : IRequestService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public RequestService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync(bool trackChanges)
        {
            var requests = await _repository.Request.GetAllRequestsAsync(trackChanges);
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public async Task<RequestDto> GetRequestAsync(int requestId, bool trackChanges)
        {
            var request = await _repository.Request.GetRequestAsync(requestId, trackChanges);
            if (request == null)
                throw new ArgumentException("Request not found.");

            return _mapper.Map<RequestDto>(request);
        }

        public async Task<IEnumerable<RequestDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var requests = await _repository.Request.GetByIdsAsync(ids, trackChanges);

            if (ids.Count() != requests.Count())
                throw new CollectionByIdsBadRequestException();

            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public IEnumerable<RequestDto> GetRequestsByRoomId(int roomId, bool roomTrackChanges, bool requestTrackChanges)
        {
            var room = _repository.Room.GetRoomAsync(roomId, roomTrackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);

            var requests = _repository.Request.GetRequestsByRoomIdAsync(roomId, requestTrackChanges);
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public async Task DeleteRequest(int roomId, int requestId, bool roomTrackChanges, bool requestTrackChanges)
        {
            var room = _repository.Room.GetRoomAsync(roomId, roomTrackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);

            var request = await _repository.Request.GetRequestAsync(requestId, requestTrackChanges);
            if (request == null)
                throw new RequestNotFoundException(requestId);

            _repository.Request.DeleteRequest(request);

            await _repository.SaveAsync();
        }

        public async Task<RequestDto> CreateRequestForRoomByInstructorIdAsync(RequestDto requestDto, int instructorId, int roomId, bool trackChanges)
        {
            var room = _repository.Room.GetRoomAsync(roomId, trackChanges);
            var instructor = _repository.Instructor.GetInstructorAsync(instructorId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            if (instructor is null)
                throw new ArgumentException("hoca yok amk");
            var requestEntity = _mapper.Map<Request>(requestDto);

            requestEntity.RoomId = roomId;
            requestEntity.InstructorId = instructorId;

            _repository.Request.CreateRequestByInstructorId(instructorId, requestEntity);

            await _repository.SaveAsync();

            return _mapper.Map<RequestDto>(requestEntity);
        }
        public async Task<RequestDto> CreateRequestForRoomByStudentIdAsync(RequestDto requestDto, int studentId, int roomId, bool trackChanges)
        {
            var room = _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            var instructor = _repository.Student.GetStudentAsync(studentId, trackChanges);
            if (instructor is null)
                throw new ArgumentException("sen kimsin amk");
            var requestEntity = _mapper.Map<Request>(requestDto);

            requestEntity.RoomId = roomId;
            requestEntity.StudentId = studentId;

            _repository.Request.CreateRequestByInstructorId(studentId, requestEntity);

            await _repository.SaveAsync();

            return _mapper.Map<RequestDto>(requestEntity);
        }


        public async Task UpdateRequestAsync(int requestId, RequestDto requestForUpdate, bool trackChanges)
        {
            var request = await _repository.Request.GetRequestAsync(requestId, trackChanges);
            if (request == null)
                throw new ArgumentException("Request not found.");

            _mapper.Map(requestForUpdate, request);

            await _repository.SaveAsync();
        }

      
    }
}
