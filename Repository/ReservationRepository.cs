using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // Get all reservations
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(r => r.EventDate) // Reservations sorted by event date
                .ToListAsync();
        }

        // Get a specific reservation by ID
        public async Task<Reservation> GetReservationAsync(int reservationId, bool trackChanges)
        {
            return await FindByCondition(r => r.ReservationId == reservationId, trackChanges)
                .SingleOrDefaultAsync();
        }

        // Get reservations by user ID (teacher or club president)
        public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId, bool trackChanges)
        {
            return await FindByCondition(r => r.CreatedBy == userId, trackChanges)
                .OrderBy(r => r.EventDate) // Reservations sorted by event date
                .ToListAsync();
        }

        // Create a new reservation
        public void CreateReservation(Reservation reservation)
        {
            Create(reservation);
        }

        // Update an existing reservation
        public async Task UpdateReservationAsync(Reservation reservation)
        {
            var existingReservation = await GetReservationAsync(reservation.ReservationId, true);
            if (existingReservation != null)
            {
                existingReservation.EventDate = reservation.EventDate;
                existingReservation.StartTime = reservation.StartTime;
                existingReservation.EndTime = reservation.EndTime;
                existingReservation.ReservationType = reservation.ReservationType;
                existingReservation.LectureCode = reservation.LectureCode;
                existingReservation.RoomId = reservation.RoomId;

                Update(existingReservation);
            }
        }

        // Delete a reservation
        public async Task DeleteReservationAsync(Reservation reservation)
        {
            var existingReservation = await GetReservationAsync(reservation.ReservationId, true);
            if (existingReservation != null)
            {
                Delete(existingReservation);
            }
        }
    }
}
