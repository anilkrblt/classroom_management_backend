using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // Get all requests
        public async Task<IEnumerable<Request>> GetAllRequestsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(req=> req.Room)
                .OrderBy(r => r.CreatedAt) 
                .ToListAsync();
        }

        // Get a specific request by ID
        public async Task<Request> GetRequestAsync(int requestId, bool trackChanges)
        {
            return await FindByCondition(r => r.RequestId == requestId, trackChanges)
                .Include(req=> req.Room)
                .SingleOrDefaultAsync();
        }

        // Get requests by room ID
        public async Task<IEnumerable<Request>> GetRequestsByRoomIdAsync(int roomId, bool trackChanges)
        {
            return await FindByCondition(r => r.RoomId == roomId, trackChanges)
                .Include(req=> req.Room)
                .OrderBy(r => r.CreatedAt) 
                .ToListAsync();
        }

        // Get requests by user ID
        public async Task<IEnumerable<Request>> GetRequestsByUserIdAsync(int userId, bool trackChanges)
        {
            return await FindByCondition(r => r.UserId == userId, trackChanges)
                .Include(req=> req.Room)
                .OrderBy(r => r.CreatedAt) 
                .ToListAsync();
        }

        // Create a new request
        public void CreateRequest(Request request)
        {
            Create(request);
        }

        // Update an existing request
        public async Task UpdateRequestAsync(Request request)
        {
            var existingRequest = await GetRequestAsync(request.RequestId, true);
            if (existingRequest != null)
            {
                existingRequest.Type = request.Type;
                existingRequest.Content = request.Content;
                existingRequest.Status = request.Status;
                existingRequest.RoomId = request.RoomId;
                existingRequest.UpdatedAt = request.UpdatedAt;

                Update(existingRequest);
            }
        }

        // Delete a request
        public async Task DeleteRequestAsync(Request request)
        {
            var existingRequest = await GetRequestAsync(request.RequestId, true);
            if (existingRequest != null)
            {
                Delete(existingRequest);
            }
        }
    }
}
