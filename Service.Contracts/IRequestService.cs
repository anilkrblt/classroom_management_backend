using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IRequestService
    {
        // Get all requests
        Task<IEnumerable<RequestDto>> GetAllRequestsAsync(bool trackChanges);

        // Get a specific request by ID
        Task<RequestDto> GetRequestByIdAsync(int requestId, bool trackChanges);

        // Get requests by room ID
        Task<IEnumerable<RequestDto>> GetRequestsByRoomIdAsync(int roomId, bool trackChanges);

        // Get requests by user ID
        Task<IEnumerable<RequestDto>> GetRequestsByUserIdAsync(int userId, bool trackChanges);

        // Create a new request
        Task CreateRequestAsync(RequestDto requestDto);

        // Update an existing request
        Task UpdateRequestAsync(int requestId, RequestDto requestDto);

        // Delete a request
        Task DeleteRequestAsync(int requestId);
    }
}
