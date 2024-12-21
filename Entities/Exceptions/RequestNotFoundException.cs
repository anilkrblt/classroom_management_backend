using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RequestNotFoundException : NotFoundException
    {
        public RequestNotFoundException(int requestId) :
         base($"The request with id: {requestId} doesn't exist in the  database.")
        {

        }


    }
}