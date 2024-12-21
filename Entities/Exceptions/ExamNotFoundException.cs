using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ExamNotFoundException : NotFoundException
    {
        public ExamNotFoundException(int examId)
        : base($"The exam with id: {examId} doesn't exist in the  database.")
        {
        }
    }
}