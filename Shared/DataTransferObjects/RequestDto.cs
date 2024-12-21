using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RequestDto
    {
        public int RequestId { get; set; }
        public string? Type { get; set; }

        public string? Content { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int StudentId { get; set; }
        public int InstructorId { get; set; }


    }

    public record RequestCreationForStudentDto
    {
        string? Type { get; set; }
        string? Content { get; set; }
        string? Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        int StudentId { get; set; }
    };

    public record RequestCreationForInstructorDto
    {
        string? Type { get; set; }
        string? Content { get; set; }
        string? Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        int InstructorId { get; set; }
    };







}