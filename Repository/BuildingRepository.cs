using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BuildingRepository : RepositoryBase<Building>, IBuildingRepository
    {
        public BuildingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Building>> GetAllBuildingsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(b => b.Name) // Binaları isme göre sıralar
                .ToListAsync();
        }

        public async Task<Building> GetBuildingAsync(int buildingId, bool trackChanges)
        {
            return await FindByCondition(b => b.BuildingId == buildingId, trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
