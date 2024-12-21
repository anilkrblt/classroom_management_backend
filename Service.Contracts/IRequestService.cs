using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRequestService
    {

        Task<IEnumerable<RequestDto>> GetAllRequestsAsync(bool trackChanges);
        Task<RequestDto> GetRequestAsync(int requestId, bool trackChanges);
        Task<IEnumerable<RequestDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        IEnumerable<RequestDto> GetRequestsByRoomId(int roomId, bool roomTrackChanges, bool requestTrackChanges);
        Task DeleteRequest(int roomId, int requestId, bool roomTrackChanges, bool requestTrackChanges);
        Task<RequestDto> CreateRequestForRoomByStudentIdAsync(RequestDto requestDto, int studentId, int roomId, bool trackChanges);

        Task<RequestDto> CreateRequestForRoomByInstructorIdAsync(RequestDto requestDto, int instructorId, int roomId, bool trackChanges);
        Task UpdateRequestAsync(int requestId, RequestDto requestForUpdate, bool trackChanges);

    }
}