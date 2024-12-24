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
        Task CreateLectureAsync(LectureDto lectureDto);

        // Update an existing lecture
        Task UpdateLectureAsync(string code, LectureDto lectureDto);

        // Delete a lecture
        Task DeleteLectureAsync(string code);
    }
}
