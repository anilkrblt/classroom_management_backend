using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ClubReservationRepository : RepositoryBase<ClubReservation>, IClubReservationRepository
    {
        public ClubReservationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateClubReservation(ClubReservation ClubReservation)
        {
            throw new NotImplementedException();
        }

        public void DeleteClubReservation(ClubReservation ClubReservation)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClubReservation>> GetAllClubReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                        .Include(cr => cr.Club)
                        .Include(cr => cr.Reservation)
                            .ThenInclude(r => r.Room)
                        .Include(cr => cr.Student)
                        .ToListAsync();
        }

        public async Task<ClubReservation> GetClubReservationAsync(int reservationId, bool trackChanges)
        {
            return await FindByCondition(r => r.ReservationId == reservationId, trackChanges)
                .Include(cr => cr.Club)
                .Include(cr => cr.Reservation)
                .SingleOrDefaultAsync();
        }




        public Task UpdateClubReservationAsync(ClubReservation ClubReservation, int ClubReservationId)
        {
            throw new NotImplementedException();
        }
    }
}