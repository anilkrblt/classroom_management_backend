namespace Entities.Models
{
    public class Room
    {

        public int RoomId { get; set; }
        public string? Name { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public int BuildingId { get; set; }
        public required Building Building { get; set; }

        public required ICollection<ExamClass> ExamClasses { get; set; }
        public required ICollection<Class> Classes { get; set; }
        public required ICollection<LectureHall> LectureHalls { get; set; }
        public required ICollection<Lab> Labs { get; set; }
        public required ICollection<Reservation> Reservations { get; set; }
        public required ICollection<Request> Requests { get; set; }
        public required ICollection<LectureSession> LectureSessions { get; set; }
    }
}