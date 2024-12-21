using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ILectureHallRepository
    {
        Task<IEnumerable<LectureHall>> GetAllLectureHallsAsync(bool trackChanges);
        Task<LectureHall> GetLectureHallAsync(int lectureHallId, bool trackChanges);
        Task<IEnumerable<LectureHall>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteLectureHall(LectureHall LectureHall);

        void CreateLectureHallForDepartment(LectureHall LectureHall, int departmentId);

    }
}