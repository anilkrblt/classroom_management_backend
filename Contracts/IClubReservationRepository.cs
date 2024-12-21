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
        Task<ClubReservation> GetClubReservationAsync(int clubReservationId, bool trackChanges);
        Task<IEnumerable<ClubReservation>> GetByReservationIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteClubReservation(ClubReservation ClubReservation);

        void CreateClubReservation(ClubReservation ClubReservation);


    }
}