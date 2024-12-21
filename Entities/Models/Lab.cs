using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Lab
    {
        [Key]
        public int RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; } = null!;

        public required List<string> Equipment { get; set; }


        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
    }
}