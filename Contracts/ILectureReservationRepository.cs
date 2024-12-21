using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ILectureReservationRepository
    {
        Task<IEnumerable<LectureReservation>> GetAllLectureReservationsAsync(bool trackChanges);
        Task<LectureReservation> GetLectureReservationAsync(int reservationId, bool trackChanges);
        Task<IEnumerable<LectureReservation>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteLectureReservation(int reservationId);

        void CreateLectureReservation(int instructorId,int lectureReservation);


    }
}