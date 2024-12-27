using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    /*
    public record RequestDto
    {
        public int RequestId { get; set; }
        public string? Type { get; set; }

        public string? Content { get; set; }

        public string? Title { get; set; }

        public string? Status { get; set; }

        public string Name { get; set; }

        public string RoomName { get; set; }

        public DateTime Date { get; set; }

        public string Photos { get; set; }

        public string SolveDescription { get; set; }


    }*/
    public record RequestDto
    {
        public int RequestId { get; set; }
        public string Type { get; set; } // [Required]
        public string Content { get; set; } // [Required]
        public string? Title { get; set; }
        public string Status { get; set; } // [Required]
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Photos { get; set; }
        public string SolveDescription { get; set; }
    }


    public record RequestCreationForStudentDto
    {
        public string UserId { get; set; }

        public string Title { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string photos { get; set; }
    };









}