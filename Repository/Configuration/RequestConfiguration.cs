using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> Request)
        {
        Request.HasData
        (
            new Request
            {
                RequestId = 1,
                Type = "Maintenance",
                Title = "We are lookin for ...",
                Content = "Fix the projector in Room 101",
                Status = "Pending",
                SolveDescription = "",
                ImagePaths = "img1.jpg,img2.jpg",           
                CreatedAt = new DateTime(2024, 12, 25),
                UpdatedAt = new DateTime(2024, 12, 25),
                RoomId = 1,
                UserName = "",
                UserId = 1
            
            }
           
        );
        }
    }
}