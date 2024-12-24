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
    public class InstructorService : IInstructorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public InstructorService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all instructors
        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync(bool trackChanges)
        {
            var instructors = await _repositoryManager.Instructor.GetAllInstructorsAsync(trackChanges);
            return _mapper.Map<IEnumerable<InstructorDto>>(instructors);
        }

        // Get a specific instructor by ID
        public async Task<InstructorDto> GetInstructorByIdAsync(int instructorId, bool trackChanges)
        {
            var instructor = await _repositoryManager.Instructor.GetInstructorAsync(instructorId, trackChanges);

            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID {instructorId} not found.");

            return _mapper.Map<InstructorDto>(instructor);
        }

        // Get lectures taught by a specific instructor
        public async Task<IEnumerable<LectureDto>> GetInstructorLecturesAsync(int instructorId, bool trackChanges)
        {
            var lectures = await _repositoryManager.Lecture.GetLecturesByInstructorIdAsync(instructorId, trackChanges);

            if (!lectures.Any())
                throw new KeyNotFoundException($"No lectures found for Instructor with ID {instructorId}.");

            return _mapper.Map<IEnumerable<LectureDto>>(lectures);
        }

        // Create a new instructor
        public async Task CreateInstructorAsync(InstructorDto instructorDto)
        {
            var instructor = _mapper.Map<Instructor>(instructorDto);

            _repositoryManager.Instructor.CreateInstructor(instructor);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing instructor
        public async Task UpdateInstructorAsync(int instructorId, InstructorDto instructorDto)
        {
            var instructor = await _repositoryManager.Instructor.GetInstructorAsync(instructorId, trackChanges: true);

            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID {instructorId} not found.");

            _mapper.Map(instructorDto, instructor);
            await _repositoryManager.SaveAsync();
        }

        // Delete an instructor
        public async Task DeleteInstructorAsync(int instructorId)
        {
            var instructor = await _repositoryManager.Instructor.GetInstructorAsync(instructorId, trackChanges: true);

            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID {instructorId} not found.");

            await _repositoryManager.Instructor.DeleteInstructorAsync(instructor);
            await _repositoryManager.SaveAsync();
        }
    }
}
