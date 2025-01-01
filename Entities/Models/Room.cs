using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public enum RoomType
    {
        Classroom,
        ElectricLab,
        PcLab,
        GenLab,
        GidaLab,
        Amfi,
        MachLab

    }

    [Table("Room")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Range(0, 999)]
        public int Capacity { get; set; }

        public int ExamCapacity { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        public bool IsProjectorWorking { get; set; } // Indicates whether the projector is functional

        public string Equipment { get; set; } // JSON to store additional equipment details

        [ForeignKey("Building")]
        public int BuildingId { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Building Building { get; set; }
        public Department Department { get; set; }
        public ICollection<LectureSession> LectureSessions { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Request> Requests { get; set; }

        
    }
}