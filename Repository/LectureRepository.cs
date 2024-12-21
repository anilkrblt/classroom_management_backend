using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureRepository : RepositoryBase<Lecture>, ILectureRepository
    {
        public LectureRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Lecture>> GetAllLecturesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(l => l.Name) 
                .ToListAsync();
        }

        public async Task<Lecture> GetLectureAsync(string lectureCode, bool trackChanges)
        {
            return await FindByCondition(l => l.Code == lectureCode, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Lecture>> GetByIdsAsync(IEnumerable<string> ids, bool trackChanges)
        {
            return await FindByCondition(l => ids.Contains(l.Code), trackChanges)
                .ToListAsync();
        }

        public void DeleteLecture(Lecture lecture)
        {
            Delete(lecture);
        }

        public void CreateLecture(int departmentId, Lecture lecture)
        {
            lecture.DepartmentId = departmentId; // Dersi belirli bir bölümle ilişkilendirir
            Create(lecture);
        }
    }
}
