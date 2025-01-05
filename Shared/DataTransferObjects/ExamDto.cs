using System;
using System.Collections.Generic;
using System.Linq;
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

        public string Type { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }

    public record ExamScheduleDto
    {

        public string LectureCode { get; set; }
        public List<string> RoomNames { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public DateTime Date { get; set; }

    }


    public record ExamSessionPostDto
    {

        public string LectureCode { get; set; }
        public int StudentCount { get; set; }
        public int Duration { get; set; }
        public int Grade { get; set; }
    }

    public record ExamSessionCreate
    {
        public List<ExamSessionPostDto> exams { get; set; }
        public List<ExamRoomDto> rooms { get; set; }
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