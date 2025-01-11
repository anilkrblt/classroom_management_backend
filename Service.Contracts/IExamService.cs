using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IExamService
    {
        // Get all exams
        Task<IEnumerable<ExamDto>> GetAllExamsAsync(bool trackChanges);

        // Get a specific exam by ID
        Task<ExamDto> GetExamByIdAsync(int examId, bool trackChanges);


        // Create a new exam

        Task<List<ExamListDto>> CreateAllExamsAsync(string type);
        Task<List<ExamScheduleExtendedDto>> CreateAllExamSessionsAsync(ExamSessionCreateDto dto);


        Task CreateExamAsync(ExamDto examDto);

        // Update an existing exam
        Task UpdateExamAsync(int examId, ExamDto examDto);

        // Delete an exam
        Task DeleteExamAsync(int examId);
    }
}
