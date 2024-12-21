namespace Shared.DataTransferObjects
{
    public record LectureSessionDto
    {
        
        public int LectureSessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LectureCode { get; set; } = null!;
        public int InstructorId { get; set; }
        public int RoomId {  get; set; }
        
    }
}