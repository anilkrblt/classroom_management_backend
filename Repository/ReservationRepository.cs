using System;
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

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(r => r.EventDate) .ToListAsync();
        }

        public async Task<Reservation> GetReservationAsync(int reservationId, bool trackChanges)
        {
            return await FindByCondition(r => r.ReservationId == reservationId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(r => ids.Contains(r.ReservationId), trackChanges).ToListAsync();
        }

        public void DeleteReservation(Reservation reservation)
        {
            Delete(reservation);
        }

        public void CreateReservationForRoom(int roomId, Reservation reservation)
        {
            reservation.RoomId = roomId; 
            Create(reservation);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(int roomId, bool trackChanges)
        {
                return await FindByCondition(r => r.RoomId == roomId, trackChanges).ToListAsync();
        }
    }
}
