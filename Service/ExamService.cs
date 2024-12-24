using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ExamService : IExamService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ExamService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all exams
        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync(bool trackChanges)
        {
            var exams = await _repositoryManager.Exam.GetAllExamsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        // Get a specific exam by ID
        public async Task<ExamDto> GetExamByIdAsync(int examId, bool trackChanges)
        {
            var exam = await _repositoryManager.Exam.GetExamAsync(examId, trackChanges);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {examId} not found.");

            return _mapper.Map<ExamDto>(exam);
        }

        // Create a new exam
        public async Task CreateExamAsync(ExamDto examDto)
        {
            var exam = _mapper.Map<Exam>(examDto);
            _repositoryManager.Exam.CreateExam(exam);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing exam
        public async Task UpdateExamAsync(int examId, ExamDto examDto)
        {
            var exam = await _repositoryManager.Exam.GetExamAsync(examId, trackChanges: true);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {examId} not found.");

            _mapper.Map(examDto, exam);
            await _repositoryManager.SaveAsync();
        }

        // Delete an exam
        public async Task DeleteExamAsync(int examId)
        {
            var exam = await _repositoryManager.Exam.GetExamAsync(examId, trackChanges: true);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {examId} not found.");

            await _repositoryManager.Exam.DeleteExamAsync(exam);
            await _repositoryManager.SaveAsync();
        }
    }
}
