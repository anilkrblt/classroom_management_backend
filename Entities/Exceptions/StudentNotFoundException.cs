using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(int studentId)
        : base($"The student with id: {studentId} doesn't exist in the  database.")
        {
            
        }
    }
}