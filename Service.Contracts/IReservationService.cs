using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges);
        Task<ReservationDto> GetReservationAsync(int ReservationId, bool trackChanges);
        Task<IEnumerable<ReservationDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        IEnumerable<ReservationDto> GetReservationsByRoomId(int roomId, bool trackChanges);
        Task DeleteReservation(int roomId, int ReservationId, bool trackChanges);
        Task<ReservationDto> CreateReservationForRoomAsync(ReservationDto Reservation, int roomId);
        Task UpdateReservationAsync(int ReservationId, ReservationDto ReservationForUpdate, bool trackChanges);


        Task<IEnumerable<ClubReservationDto>> GetAllClubReservationsAsync(bool trackChanges);
        Task<ClubReservationDto> GetClubReservationByReservationIdAsync(int reservationId, bool trackChanges);

        Task DeleteClubReservationForReservationAsync(int reservationId, bool trackChanges);

        Task<ClubReservationDto> CreateClubReservationForReservation(int reservationId,
                                            ClubReservationDto clubReservationForCreation,
                                            bool trackChanges);


        Task<IEnumerable<LectureReservationDto>> GetAllLectureReservationsAsync(bool trackChanges);
        Task<LectureReservationDto> GetLectureReservationByReservationIdAsync(int reservationId, bool trackChanges);

        Task DeleteLectureReservationForReservationAsync(int reservationId, bool trackChanges);

        /*Task<LectureReservationDto> CreateLectureReservationForReservation(int reservationId,
                                            LectureReservationDto clubReservationForCreation,
                                            bool trackChanges);*/


    }
}