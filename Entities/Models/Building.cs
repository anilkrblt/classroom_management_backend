using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Building")]
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}