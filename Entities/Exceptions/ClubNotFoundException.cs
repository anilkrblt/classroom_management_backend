using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClubNotFoundException : NotFoundException
    {
        public ClubNotFoundException(int clubId)
        : base($"The club with id: {clubId} doesn't exist in the  database.")
        {
        }
    }
}