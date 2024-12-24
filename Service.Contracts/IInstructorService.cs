using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IInstructorService
    {
        // Get all instructors
        Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync(bool trackChanges);

        // Get a specific instructor by ID
        Task<InstructorDto> GetInstructorByIdAsync(int instructorId, bool trackChanges);

        // Get lectures taught by a specific instructor
        Task<IEnumerable<LectureDto>> GetInstructorLecturesAsync(int instructorId, bool trackChanges);

        // Create a new instructor
        Task CreateInstructorAsync(InstructorDto instructorDto);

        // Update an existing instructor
        Task UpdateInstructorAsync(int instructorId, InstructorDto instructorDto);

        // Delete an instructor
        Task DeleteInstructorAsync(int instructorId);
    }
}
