
namespace Shared.DataTransferObjects
{

    public record RoomDto
    {
        public int RoomId { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int ExamCapacity { get; set; }
        public bool IsProjectorWorking { get; set; }
        public bool IsActive { get; set; }
        public int RoomType { get; set; }

        public string DepartmentName { get; set; }
        public string BuildingName { get; set; }
        public List<LectureInfoDto> Lectures { get; set; }
    }

    public record LectureInfoDto
    {
        public string LectureName { get; set; }
        public string TeacherName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DepartmentName { get; set; }
        public string? DayOfWeek { get; set; }

    }

    public record RoomCreationForBuildingDto
    {
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int ExamCapacity { get; set; }
        public bool IsProjectorWorking { get; set; }
        public string? Equipment { get; set; }

        public bool IsActive { get; set; }
        public int RoomType { get; set; } // 0: Classroom, 1: ElectricLab vs. Enum için int
        public int DepartmentId { get; set; }
        public int BuildingId { get; set; }
    }


    public record RoomUpdateForBuildingDto
    {
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int ExamCapacity { get; set; }
        public bool IsProjectorWorking { get; set; }
        public string? Equipment { get; set; }

        public bool IsActive { get; set; }
        public int RoomType { get; set; } // 0: Classroom, 1: ElectricLab vs. Enum için int
        public int DepartmentId { get; set; }
        public int BuildingId { get; set; }
    }

}