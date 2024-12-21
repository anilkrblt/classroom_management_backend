using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(int roomId) : 
        base($"The room with id: {roomId} doesn't exist in the  database.")
        {

        }
    }
}