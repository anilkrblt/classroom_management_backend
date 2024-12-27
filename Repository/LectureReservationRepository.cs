using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureReservationRepository : RepositoryBase<LectureReservation>, ILectureReservationRepository
    {
        public LectureReservationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public void CreateLectureReservation(LectureReservation LectureReservation) => Create(LectureReservation);

        public void DeleteLectureReservationAsync(LectureReservation LectureReservation) => Delete(LectureReservation);

        public async Task<IEnumerable<LectureReservation>> GetAllLectureReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
            }

        public async Task<LectureReservation> GetLectureReservationAsync(string code, bool trackChanges)
        {
            return await FindByCondition(lr => lr.Code == code, trackChanges).SingleOrDefaultAsync();
        }

        public Task<IEnumerable<LectureReservation>> GetLectureReservationsByInstructorIdAsync(int instructorId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLectureReservationAsync(LectureReservation LectureReservation, string code)
        {
            throw new NotImplementedException();
        }
    }
}