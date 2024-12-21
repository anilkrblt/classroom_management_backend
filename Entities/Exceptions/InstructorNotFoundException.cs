using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class InstructorNotFoundException : NotFoundException
    {
        public InstructorNotFoundException(int instructorId)
        : base($"The instructor with id: {instructorId} doesn't exist in the  database.")
        {
        }
    }
}