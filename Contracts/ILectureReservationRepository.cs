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

        Task<LectureReservation> GetLectureReservationAsync(string code, bool trackChanges);

        void CreateLectureReservation(LectureReservation LectureReservation);
        Task<IEnumerable<LectureReservation>> GetLectureReservationsByInstructorIdAsync(int instructorId, bool trackChanges);

        Task UpdateLectureReservationAsync(LectureReservation LectureReservation, string code);

        void DeleteLectureReservationAsync(LectureReservation LectureReservation);
        
    }
}