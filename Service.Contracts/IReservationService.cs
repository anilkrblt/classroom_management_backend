using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IReservationService
    {
        // Get all reservations
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges);

        Task<IEnumerable<ClubReservationGetDto>> GetAllClubReservations(bool trackChanges);


        // Get a specific reservation by ID
        Task<ReservationDto> GetReservationByIdAsync(int reservationId, bool trackChanges);

        // Get reservations by user ID
        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId, bool trackChanges);

        Task<ClubReservationDto> CreateClubReservationAsync(ClubReservationDto reservationDto);


        // Create a new reservation
        Task CreateReservationAsync(ReservationDto reservationDto);

        // Update an existing reservation
        Task UpdateReservationAsync(int reservationId, ReservationDto reservationDto);

        // Delete a reservation
        Task DeleteReservationAsync(int reservationId);
    }
}
