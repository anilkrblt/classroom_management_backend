using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureHallRepository : RepositoryBase<LectureHall>, ILectureHallRepository
    {
        public LectureHallRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<LectureHall>> GetAllLectureHallsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(lh => lh.Room.Name)
                .ToListAsync();
        }

        public async Task<LectureHall> GetLectureHallAsync(int lectureHallId, bool trackChanges)
        {
            return await FindByCondition(lh => lh.RoomId == lectureHallId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<LectureHall>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(lh => ids.Contains(lh.RoomId), trackChanges)
                .ToListAsync();
        }

        public void DeleteLectureHall(LectureHall lectureHall)
        {
            Delete(lectureHall);
        }

        public void CreateLectureHallForDepartment(LectureHall lectureHall, int departmentId)
        {
            lectureHall.DepartmentId = departmentId; // Amfiyi belirli bir bölümle ilişkilendirir
            Create(lectureHall);
        }
    }
}
