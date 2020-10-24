using DigitalMind.YoYoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalMind.YoYoApp.Infra.EFMappings
{
    public class ShuttleMap : IEntityTypeConfiguration<Shuttle>
    {
        public void Configure(EntityTypeBuilder<Shuttle> builder)
        {
            builder.Property(key => key.Id).ValueGeneratedOnAdd();

           // builder.HasData(LoadJson());
        }
      
    }
}
