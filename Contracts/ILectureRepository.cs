using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILectureRepository
    {
    
        Task<IEnumerable<Lecture>> GetAllLecturesAsync(bool trackChanges);

        Task<Lecture> GetLectureAsync(string code, bool trackChanges);

        void CreateLecture(Lecture lecture);
        Task<IEnumerable<Lecture>> GetLecturesByInstructorIdAsync(int instructorId, bool trackChanges);

        Task UpdateLectureAsync(Lecture lecture, string code);

        Task DeleteLectureAsync(Lecture lecture);
    }
}
