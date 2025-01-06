namespace Shared.DataTransferObjects
{
    public record LectureSessionDto
    {

        public int LectureSessionId { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string LectureCode { get; set; }
        public string LectureName { get; set; }
        public string InstructorName { get; set; }
        public string RoomName { get; set; }


    }
    public record LectureSessionUpdateDto
    {

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string RoomName { get; set; }


    }

}