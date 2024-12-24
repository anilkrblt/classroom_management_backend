using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ILectureSessionRepository
    {
        Task<IEnumerable<LectureSession>> GetAllLectureSessionsAsync(bool trackChanges);
        Task<LectureSession> GetLectureSessionAsync(int lectureSessionId, bool trackChanges);
        Task<IEnumerable<LectureSession>> GetLectureSessionByInstructorIdAsync(int instructorId, bool trackChanges);
        void CreateLectureSession(LectureSession lectureSession);
        Task UpdateLectureSessionAsync(LectureSession lectureSession);
        Task DeleteLectureSessionAsync(LectureSession lectureSession);
    }
}
