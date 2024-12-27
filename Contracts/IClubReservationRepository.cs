using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IClubReservationRepository
    {
        
        Task<IEnumerable<ClubReservation>> GetAllClubReservationsAsync(bool trackChanges);

        Task<ClubReservation> GetClubReservationAsync(int ClubReservationId, bool trackChanges);

        void CreateClubReservation(ClubReservation ClubReservation);

        Task UpdateClubReservationAsync(ClubReservation ClubReservation, int ClubReservationId);

        void DeleteClubReservation(ClubReservation ClubReservation);
        
    }
}