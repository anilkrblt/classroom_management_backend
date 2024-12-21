using System;
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

        public async Task<IEnumerable<Request>> GetAllRequestsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(r => r.UpdatedAt)
                .ToListAsync();
        }

        public async Task<Request> GetRequestAsync(int requestId, bool trackChanges)
        {
            return await FindByCondition(r => r.RequestId == requestId, trackChanges).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Request>> GetRequestsByRoomIdAsync(int roomId, bool trackChanges)
        {
            return await FindByCondition(r => r.RoomId == roomId, trackChanges).ToListAsync();

        }

        public async Task<IEnumerable<Request>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(r => ids.Contains(r.RequestId), trackChanges)
                .ToListAsync();
        }

        public void DeleteRequest(Request request)
        {
            Delete(request);
        }

        public void CreateRequestByStudentId(int id, Request request)
        {
            request.StudentId = id;
            Create(request);
        }

        public void CreateRequestByInstructorId(int id, Request request)
        {
            request.InstructorId = id;
            Create(request);
        }

       
    }
}

