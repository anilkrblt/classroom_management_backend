using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ExamDto
    {

        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public record ExamSessionCreateDto
    {
        public int Year { get; set; }
        public string Term { get; set; }
        public string Type { get; set; }
        public List<string> Dates { get; set; }


    }
    public record ExamCreateDto
    {
        public int Year { get; set; }
        public string Term { get; set; }
        public string Type { get; set; }
    }
    public record ExamSessionDeleteDto
    {
        public int Year { get; set; }
        public string Type { get; set; }
        public string Term { get; set; }
    }


    public record ExamScheduleAndMoreDto
    {
        [JsonPropertyName("planning")]
        public List<ExamScheduleDto> ExamSchedule { get; set; }
        [JsonPropertyName("unscheduled_exams")]
        public List<string> UnAssignedLectures { get; set; }
    }
    public record ExamScheduleExtendedAndMoreDto
    {
        [JsonPropertyName("planning")]
        public List<ExamScheduleExtendedDto> ExamSchedule { get; set; }
        [JsonPropertyName("unscheduled_exams")]
        public List<UnAssignedLecturesDto> UnAssignedLectures { get; set; }
    }
    public record UnAssignedLecturesDto
    {
        public string LectureName { get; set; }
        public string LectureCode { get; set; }
        public string DepartmentName { get; set; }
        public int Grade { get; set; }
    }
    public record ExamScheduleDto
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LectureCode { get; set; }
        public string RoomNames { get; set; }
        [JsonPropertyName("Duration (minute)")]
        public int Duration { get; set; }


    }
    public record ExamScheduleExtendedDto
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LectureCode { get; set; }
        public string LectureName { get; set; }
        public int Grade { get; set; }
        public string DepartmentName { get; set; }
        public string RoomNames { get; set; }
        public int Duration { get; set; }


    }


    public record ExamSessionPostDto
    {
        [JsonPropertyName("lecture_code")]
        public string LectureCode { get; set; }

        [JsonPropertyName("student_count")]
        public int StudentCount { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("grade")]
        public int Grade { get; set; }
    }


    public record ExamSessionCreate
    {
        [JsonPropertyName("exams")]
        public List<ExamSessionPostDto> Exams { get; set; }

        [JsonPropertyName("rooms")]
        public List<ExamRoomDto> Rooms { get; set; }

        [JsonPropertyName("day_time")]
        public List<ExamDateRangeDto> DateRange { get; set; }
    }



    public class ExamDateRangeDto
    {
        [JsonPropertyName("date")]
        public string Tarih { get; set; }

        [JsonPropertyName("start_times")]
        public List<string> BaslangicSaatleri { get; set; }
    }


















    public record ExamListDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Duration { get; set; }

        public string LectureCode { get; set; }
        public string DepartmentName { get; set; }

    }

}