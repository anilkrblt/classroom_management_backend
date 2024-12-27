using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ExamDto
    {

        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }




    }
}