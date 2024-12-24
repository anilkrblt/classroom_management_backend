using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasData
            (
            new Building
            {
                BuildingId = 1,
                Name = "A",
            },
            new Building
            {
                BuildingId = 2,
                Name = "B",
            }
            );
        }
    }
}