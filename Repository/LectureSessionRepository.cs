using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureSessionRepository : RepositoryBase<LectureSession>, ILectureSessionRepository
    {
        public LectureSessionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<LectureSession>> GetAllLectureSessionsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(ls => ls.StartTime)
                .ToListAsync();
        }

        public async Task<LectureSession> GetLectureSessionAsync(int lectureSessionId, bool trackChanges)
        {
            return await FindByCondition(ls => ls.LectureSessionId == lectureSessionId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<LectureSession>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(ls => ids.Contains(ls.LectureSessionId), trackChanges).ToListAsync();
        }

        public void DeleteLectureSession(LectureSession lectureSession)
        {
            Delete(lectureSession);
        }

        public void CreateLectureSession(int roomId, int instructorId, string lectureCode, LectureSession lectureSession)
        {
            lectureSession.RoomId = roomId;
            lectureSession.InstructorId = instructorId;
            lectureSession.LectureCode = lectureCode;
            Create(lectureSession);
        }
    }
}

