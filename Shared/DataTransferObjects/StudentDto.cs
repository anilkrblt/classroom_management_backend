namespace Shared.DataTransferObjects
{
    public record StudentDto
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DepartmentName { get; set; }
        public int Grade { get; set; }
        public List<UserLecturesDto> Schedule { get; set; }
    }
    public record UserLecturesDto
    {
        public string LectureCode { get; set; }
        public string LectureName { get; set; }
        public string RoomName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayOfWeek { get; set; }

    }

    public record StudentLoginDto
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public int Grade { get; set; }
        public List<StudentClubs> StudentClubs { get; set; }
        public List<UserLecturesDto> Schedule { get; set; }
    }
    public record StudentClubs
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubShorcut { get; set; }
        public bool IsManager { get; set; }
    }


    public record StudentWithLecturesDto
    {
        int StudentId { get; set; }
        public List<Dictionary<string, string>> Lectures { get; set; }

    }



}
