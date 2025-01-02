using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects
{
    public record RequestDto
    {
        public int RequestId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string? Title { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Photos { get; set; }
        public string SolveDescription { get; set; }
    }




    public record RequestCreationDto
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string? Title { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        [MaxLength(3, ErrorMessage = "En fazla 3 fotoğraf yükleyebilirsiniz.")]
        public List<IFormFile>? PhotoFiles { get; set; }
    }

}