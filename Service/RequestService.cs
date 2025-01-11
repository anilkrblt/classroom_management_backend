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
    public class RequestService : IRequestService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public RequestService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync(bool trackChanges)
        {
            var requests = await _repositoryManager.Request.GetAllRequestsAsync(trackChanges);
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public async Task<RequestDto> GetRequestByIdAsync(int requestId, bool trackChanges)
        {
            var request = await _repositoryManager.Request.GetRequestAsync(requestId, trackChanges);

            if (request == null)
                throw new KeyNotFoundException($"Request with ID {requestId} not found.");

            return _mapper.Map<RequestDto>(request);
        }

        public async Task<IEnumerable<RequestDto>> GetRequestsByRoomIdAsync(int roomId, bool trackChanges)
        {
            var requests = await _repositoryManager.Request.GetRequestsByRoomIdAsync(roomId, trackChanges);

            if (!requests.Any())
                throw new KeyNotFoundException($"No requests found for Room with ID {roomId}.");

            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public async Task<IEnumerable<RequestDto>> GetRequestsByUserIdAsync(int userId, bool trackChanges)
        {
            var requests = await _repositoryManager.Request.GetRequestsByUserIdAsync(userId, trackChanges);

            if (!requests.Any())
                throw new KeyNotFoundException($"No requests found for User with ID {userId}.");

            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }



        public async Task CreateRequestAsync(string fileName, RequestCreationDto dto)
        {

            var requestEntity = new Request
            {
                Type = dto.Type,
                Content = dto.Content,
                Title = dto.Title,
                Status = "pending",
                UserName = dto.UserName,
                UserId = dto.UserId,
                RoomId = dto.RoomId,
                SolveDescription = "",
                ImagePaths = fileName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repositoryManager.Request.CreateRequest(requestEntity);
            await _repositoryManager.SaveAsync();
            
        }

        public async Task UpdateRequestAsync(int requestId, RequestDto requestDto)
        {
            var request = await _repositoryManager.Request.GetRequestAsync(requestId, true);

            if (request == null)
                throw new KeyNotFoundException($"Request with ID {requestId} not found.");

            _mapper.Map(requestDto, request);
            await _repositoryManager.SaveAsync();
        }



        public async Task UpdateRequestStatusAsync(int requestId, RequestStatusDto requestDto)
        {
            var request = await _repositoryManager.Request.GetRequestAsync(requestId, true);

            if (request == null)
                throw new KeyNotFoundException($"Request with ID {requestId} not found.");
            request.Status = requestDto.Status;
            request.SolveDescription = requestDto.SolveDescription;

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteRequestAsync(int requestId)
        {
            var request = await _repositoryManager.Request.GetRequestAsync(requestId, true);

            if (request == null)
                throw new KeyNotFoundException($"Request with ID {requestId} not found.");

            await _repositoryManager.Request.DeleteRequestAsync(request);
            await _repositoryManager.SaveAsync();
        }
    }
}
