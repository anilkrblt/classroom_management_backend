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

        public async Task<IEnumerable<ClubReservation>> GetAllClubReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(cr => cr.Reservation.EventDate) 
                .ToListAsync();
        }

        public async Task<ClubReservation> GetClubReservationAsync(int clubReservationId, bool trackChanges)
        {
            return await FindByCondition(cr => cr.ReservationId == clubReservationId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ClubReservation>> GetByReservationIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(cr => ids.Contains(cr.ReservationId), trackChanges)
                .ToListAsync();
        }

        public void DeleteClubReservation(ClubReservation clubReservation)
        {
            Delete(clubReservation);
        }

        public void CreateClubReservation(ClubReservation clubReservation)
        {
            Create(clubReservation);
        }
    }
}
