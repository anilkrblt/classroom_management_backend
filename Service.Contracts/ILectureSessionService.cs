using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ILectureSessionService
    {
        // Get all lecture sessions
        Task<IEnumerable<LectureSessionDto>> GetAllLectureSessionsAsync(bool trackChanges);

        // Get a specific lecture session by ID
        Task<LectureSessionDto> GetLectureSessionByIdAsync(int lectureSessionId, bool trackChanges);

        // Get lecture sessions by instructor ID
        Task<IEnumerable<LectureSessionDto>> GetLectureSessionsByInstructorIdAsync(int instructorId, bool trackChanges);

        // Create a new lecture session
        Task CreateLectureSessionAsync(LectureSessionDto lectureSessionDto);

        // Update an existing lecture session
        Task UpdateLectureSessionAsync(int lectureSessionId, LectureSessionUpdateDto lectureSessionDto);

        // Delete a lecture session
        Task DeleteLectureSessionAsync(int lectureSessionId);

        Task CreateSchedule();
    }
}
