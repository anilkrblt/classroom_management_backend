using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class LectureNotFoundException : NotFoundException
    {
        public LectureNotFoundException(string lectureCode) :
        base($"The lecture with code: {lectureCode} doesn't exist in the  database.")
        {

        }

    }
}