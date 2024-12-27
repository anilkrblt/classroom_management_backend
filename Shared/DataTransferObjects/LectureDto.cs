using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record LectureDto
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public string Department { get; init; }
        public int Grade { get; init; }
        public string Term { get; init; }

        public List<InstructorForLectureDto> Instructors { get; init; }
    }

    public record InstructorForLectureDto
    {
        public int InstructorId { get; init; }
        public string InstructorName { get; init; }
    }

    public record LectureCreateDto
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public string Department { get; init; }
        public int Grade { get; init; }
        public string Term { get; init; }
        public List<InstructorForLectureDto> Instructors { get; init; }
    }
}