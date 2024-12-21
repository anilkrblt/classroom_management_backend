using Entities.Models;

namespace Contracts
{
    public interface IRequestRepository
    {

        Task<IEnumerable<Request>> GetAllRequestsAsync(bool trackChanges);
        Task<Request> GetRequestAsync(int requestId, bool trackChanges);
        Task<IEnumerable<Request>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        Task<IEnumerable<Request>> GetRequestsByRoomIdAsync(int roomId, bool trackChanges);

        void DeleteRequest(Request request);

        void CreateRequestByStudentId(int id, Request request);

        void CreateRequestByInstructorId(int id, Request request);


    }
}