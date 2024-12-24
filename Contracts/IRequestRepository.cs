using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetAllRequestsAsync(bool trackChanges);
        Task<Request> GetRequestAsync(int requestId, bool trackChanges);

        Task<IEnumerable<Request>> GetRequestsByRoomIdAsync(int roomId, bool trackChanges);
        Task<IEnumerable<Request>> GetRequestsByUserIdAsync(int userId, bool trackChanges);

        void CreateRequest(Request request);

        Task UpdateRequestAsync(Request request);
        Task DeleteRequestAsync(Request request);
    }
}
