using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ReservationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all reservations
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges)
        {
            var reservations = await _repositoryManager.Reservation.GetAllReservationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        // Get a specific reservation by ID
        public async Task<ReservationDto> GetReservationByIdAsync(int reservationId, bool trackChanges)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, trackChanges);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");

            return _mapper.Map<ReservationDto>(reservation);
        }

        // Get reservations by user ID
        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId, bool trackChanges)
        {
            var reservations = await _repositoryManager.Reservation.GetReservationsByUserId(userId, trackChanges);

            if (!reservations.Any())
                throw new KeyNotFoundException($"No reservations found for User with ID {userId}.");

            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        // Create a new reservation
        public async Task CreateReservationAsync(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            _repositoryManager.Reservation.CreateReservation(reservation);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing reservation
        public async Task UpdateReservationAsync(int reservationId, ReservationDto reservationDto)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, trackChanges: true);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");

            _mapper.Map(reservationDto, reservation);
            await _repositoryManager.SaveAsync();
        }

        // Delete a reservation
        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, trackChanges: true);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");

            await _repositoryManager.Reservation.DeleteReservationAsync(reservation);
            await _repositoryManager.SaveAsync();
        }
    }
}
