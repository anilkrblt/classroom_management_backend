namespace Shared.DataTransferObjects
{
    public record StudentDto
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

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
