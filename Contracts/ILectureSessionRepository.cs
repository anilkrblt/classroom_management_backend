using Entities.Models;

namespace Contracts
{
    public interface ILectureSessionRepository
    {
        Task<IEnumerable<LectureSession>> GetAllLectureSessionsAsync(bool trackChanges);
        Task<LectureSession> GetLectureSessionAsync(int lectureSessionId, bool trackChanges);
        Task<IEnumerable<LectureSession>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteLectureSession(LectureSession LectureSession);

        void CreateLectureSession(int roomId, int instructorId, string lectureCode,LectureSession LectureSession);


    }
}