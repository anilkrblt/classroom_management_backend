using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBuildingService
    {
        Task<IEnumerable<BuildingDto>> GetAllBuildingsAsync(bool trackChanges);

        Task<BuildingDto> GetBuildingByIdAsync(int buildingId, bool trackChanges);


    }
}