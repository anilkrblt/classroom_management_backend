using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool trackChanges);
        Task<Reservation> GetReservationAsync(int reservationId, bool trackChanges);
        Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId, bool trackChanges);

        void CreateReservation(Reservation reservation);

        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);
    }
}
