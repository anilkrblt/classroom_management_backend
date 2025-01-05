using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ExamSessionConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.HasData
            (
               
               
            );
        }
    }
}
