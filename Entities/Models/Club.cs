using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Club")]
    public class Club
    {
        [Key]
        public int ClubId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }    
        public string NameShortcut { get; set; } 
        public string ClubLogoPath { get; set; } 

        public ICollection<ClubMembership> ClubMemberships { get; set; } 
        public ICollection<ClubReservation> ClubReservations { get; set; }

    }
}