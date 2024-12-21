using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ExamClassNotFoundException : NotFoundException
    {
        public ExamClassNotFoundException(int examClassId)
        : base($"The examclass with id: {examClassId} doesn't exist in the  database.")
        {
        }
    }
}