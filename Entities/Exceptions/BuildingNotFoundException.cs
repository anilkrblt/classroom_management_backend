using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class BuildingNotFoundException : NotFoundException
    {
        public BuildingNotFoundException(int buildingId)
        : base($"The building with id: {buildingId} doesn't exist in the  database.")
        {
        }
    }
}