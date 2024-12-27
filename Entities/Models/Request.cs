using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Request")]
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; }

        public string? Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; }
        public string SolveDescription { get; set; }
        public string ImagePaths { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserName { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
