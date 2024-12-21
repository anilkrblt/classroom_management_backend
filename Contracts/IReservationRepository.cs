using Entities.Models;

namespace Contracts
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool trackChanges);
        Task<Reservation> GetReservationAsync(int reservationId, bool trackChanges);
        Task<IEnumerable<Reservation>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(int roomId, bool trackChanges);
        void DeleteReservation(Reservation Reservation);

        void CreateReservationForRoom(int roomId, Reservation Reservation);


    }
}