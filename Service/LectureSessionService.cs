using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class LectureSessionService : ILectureSessionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public LectureSessionService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all lecture sessions
        public async Task<IEnumerable<LectureSessionDto>> GetAllLectureSessionsAsync(bool trackChanges)
        {
            var lectureSessions = await _repositoryManager.LectureSession.GetAllLectureSessionsAsync(trackChanges);
            return _mapper.Map<IEnumerable<LectureSessionDto>>(lectureSessions);
        }

        // Get a specific lecture session by ID
        public async Task<LectureSessionDto> GetLectureSessionByIdAsync(int lectureSessionId, bool trackChanges)
        {
            var lectureSession = await _repositoryManager.LectureSession.GetLectureSessionAsync(lectureSessionId, trackChanges);

            if (lectureSession == null)
                throw new KeyNotFoundException($"LectureSession with ID {lectureSessionId} not found.");

            return _mapper.Map<LectureSessionDto>(lectureSession);
        }

        // Get lecture sessions by instructor ID
        public async Task<IEnumerable<LectureSessionDto>> GetLectureSessionsByInstructorIdAsync(int instructorId, bool trackChanges)
        {
            var lectureSessions = await _repositoryManager.LectureSession.GetLectureSessionByInstructorIdAsync(instructorId, trackChanges);

            if (!lectureSessions.Any())
                throw new KeyNotFoundException($"No lecture sessions found for Instructor with ID {instructorId}.");

            return _mapper.Map<IEnumerable<LectureSessionDto>>(lectureSessions);
        }

        // Create a new lecture session
        public async Task CreateLectureSessionAsync(LectureSessionDto lectureSessionDto)
        {
            var lectureSession = _mapper.Map<LectureSession>(lectureSessionDto);

            _repositoryManager.LectureSession.CreateLectureSession(lectureSession);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing lecture session
        public async Task UpdateLectureSessionAsync(int lectureSessionId, LectureSessionDto lectureSessionDto)
        {
            var lectureSession = await _repositoryManager.LectureSession.GetLectureSessionAsync(lectureSessionId, trackChanges: true);

            if (lectureSession == null)
                throw new KeyNotFoundException($"LectureSession with ID {lectureSessionId} not found.");

            _mapper.Map(lectureSessionDto, lectureSession);
            await _repositoryManager.SaveAsync();
        }

        // Delete a lecture session
        public async Task DeleteLectureSessionAsync(int lectureSessionId)
        {
            var lectureSession = await _repositoryManager.LectureSession.GetLectureSessionAsync(lectureSessionId, trackChanges: true);

            if (lectureSession == null)
                throw new KeyNotFoundException($"LectureSession with ID {lectureSessionId} not found.");

            await _repositoryManager.LectureSession.DeleteLectureSessionAsync(lectureSession);
            await _repositoryManager.SaveAsync();
        }
    }
}
