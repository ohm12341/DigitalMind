using DigitalMind.YoYoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMind.YoYoApp.Infra.EFMappings
{
    public class AthleteMap : IEntityTypeConfiguration<Athlete>
    {
        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder.Property(key => key.Id).ValueGeneratedOnAdd();
        }
    }
}
