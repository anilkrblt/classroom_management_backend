using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
   [Table("ClubMembership")]
    public class ClubMembership
    {
        [Key]
        public int ClubMembershipId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }

        public Student Student { get; set; }
        public Club Club { get; set; }
    }
}