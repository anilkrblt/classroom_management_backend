using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }


        public int StudentId { get; set; }

        public required Student Student { get; set; }


        public string Code { get; set; }
        [ForeignKey(nameof(Code))]
        public required Lecture Lecture { get; set; }
    }
}