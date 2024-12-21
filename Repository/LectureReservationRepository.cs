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

        public async Task<IEnumerable<LectureReservation>> GetAllLectureReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(lr => lr.Reservation.EventDate)
                .ToListAsync();
        }

        public async Task<LectureReservation> GetLectureReservationAsync(int reservationId, bool trackChanges)
        {
            return await FindByCondition(lr => lr.ReservationId == reservationId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<LectureReservation>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(lr => ids.Contains(lr.InstructorId), trackChanges).ToListAsync();
        }

        public void DeleteLectureReservation(int reservationId)
        {
            var reservation = FindByCondition(lr => lr.ReservationId == reservationId, true).SingleOrDefault();
            if (reservation != null)
            {
                Delete(reservation);
            }
        }

        public void CreateLectureReservation(int instructorId, int lectureReservationId)
        {
          

            var reservation = new LectureReservation
            {
                InstructorId = instructorId,
                ReservationId = lectureReservationId,
            };

            Create(reservation);
        }

    }
}
