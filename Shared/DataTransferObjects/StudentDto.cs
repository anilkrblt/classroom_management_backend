namespace Shared.DataTransferObjects
{
    public record StudentDto
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DepartmentName { get; set; }
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
        public string UserType { get; set; }
        public string FullName { get; set; }
        public int UserNo { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public int Grade { get; set; }
    }


    public record StudentWithLecturesDto
    {
        int StudentId { get; set; }
        public List<Dictionary<string, string>> Lectures { get; set; }

    }



}
