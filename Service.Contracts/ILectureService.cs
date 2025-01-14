using Entities.Models;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ILectureService
    {
        // Get all lectures
        Task<IEnumerable<LectureDto>> GetAllLecturesAsync(bool trackChanges);

        // Get a specific lecture by code
        Task<LectureDto> GetLectureByCodeAsync(string code, bool trackChanges);

        // Create a new lecture
        Task<LectureDto> CreateLectureAsync(LectureCreateDto lectureCreateDto);
        Task CreateLectureInstructorAsync(int instructorId, string lectureCode);
        Task DeleteLectureInstructorAsync(int InstructorId, string lectureCode);


        // Update an existing lecture
        Task UpdateLectureAsync(string code, LectureUpdateDto lectureDto);

        // Delete a lecture
        Task DeleteLectureAsync(string code);
    }
}
