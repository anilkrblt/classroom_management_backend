using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class ExamClassDto
    {

        public int ExamClassId { get; set; }
        public int ExamId { get; set; }
        public int RoomId { get; set; }
    }
}