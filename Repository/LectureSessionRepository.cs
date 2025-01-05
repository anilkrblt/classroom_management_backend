using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // Get all lecture sessions
        public async Task<IEnumerable<LectureSession>> GetAllLectureSessionsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(ls => ls.Date) // Lecture sessions sorted by day of week
                .ToListAsync();
        }

        // Get a specific lecture session by ID
        public async Task<LectureSession> GetLectureSessionAsync(int lectureSessionId, bool trackChanges)
        {
            return await FindByCondition(ls => ls.LectureSessionId == lectureSessionId, trackChanges)
                .SingleOrDefaultAsync();
        }

        // Get lecture sessions by instructor ID
        public async Task<IEnumerable<LectureSession>> GetLectureSessionByInstructorIdAsync(int instructorId, bool trackChanges)
        {
            return await FindByCondition(ls => ls.InstructorId == instructorId, trackChanges)
                .OrderBy(ls => ls.Date) // Lecture sessions sorted by day of week
                .ToListAsync();
        }

        // Create a new lecture session
        public void CreateLectureSession(LectureSession lectureSession)
        {
            Create(lectureSession);
        }

        // Update an existing lecture session
        public async Task UpdateLectureSessionAsync(LectureSession lectureSession)
        {
            var existingSession = await GetLectureSessionAsync(lectureSession.LectureSessionId, true);
            if (existingSession != null)
            {
                existingSession.Date = lectureSession.Date;
                existingSession.StartTime = lectureSession.StartTime;
                existingSession.EndTime = lectureSession.EndTime;
                existingSession.LectureCode = lectureSession.LectureCode;
                existingSession.RoomId = lectureSession.RoomId;
                existingSession.InstructorId = lectureSession.InstructorId;

                Update(existingSession);
            }
        }

        public async Task DeleteLectureSessionAsync(LectureSession lectureSession)
        {
            var existingSession = await GetLectureSessionAsync(lectureSession.LectureSessionId, true);
            if (existingSession != null)
            {
                Delete(existingSession);
            }
        }
    }
}
