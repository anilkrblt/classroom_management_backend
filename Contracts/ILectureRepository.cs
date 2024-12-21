using Entities.Models;

namespace Contracts
{
    public interface ILectureRepository
    {
        Task<IEnumerable<Lecture>> GetAllLecturesAsync(bool trackChanges);
        Task<Lecture> GetLectureAsync(string lectureCode, bool trackChanges);
        Task<IEnumerable<Lecture>> GetByIdsAsync(IEnumerable<string> ids, bool trackChanges);
        void DeleteLecture(Lecture Lecture);

        void CreateLecture(int departmentId, Lecture Lecture);


    }
}