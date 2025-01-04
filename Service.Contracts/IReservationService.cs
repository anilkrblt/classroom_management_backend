using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges);
        Task<IEnumerable<ClubReservationGetDto>> GetAllClubReservations(bool trackChanges);
        Task<ReservationDto> GetReservationByIdAsync(int reservationId, bool trackChanges);
        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId, bool trackChanges);
        Task<ClubReservationDto> CreateClubReservationAsync(ClubReservationDto reservationDto);
        Task CreateLectureReservationAsync(LectureReservationCreateDto lectureReservationCreateDto);
        Task UpdateClubReservationStatusAsync(int reservationId, string status, bool trackChanges);

        Task CreateReservationAsync(ReservationDto reservationDto);
        Task UpdateReservationAsync(int reservationId, ReservationDto reservationDto);
        Task DeleteReservationAsync(int reservationId);
    }
}
