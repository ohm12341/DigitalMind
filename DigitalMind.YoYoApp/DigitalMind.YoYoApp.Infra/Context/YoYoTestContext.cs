using DigitalMind.YoYoApp.Domain.Models;
using DigitalMind.YoYoApp.Infra.EFMappings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalMind.YoYoApp.Infra.Context
{
    public class YoYoTestDbContext : DbContext
    {
        public YoYoTestDbContext(DbContextOptions<YoYoTestDbContext> options) : base(options)
        {
        }

        public DbSet<Shuttle> Shuttles { get; set; }
        public DbSet<Athlete> Athletes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShuttleMap());
            modelBuilder.ApplyConfiguration(new AthleteMap());

            base.OnModelCreating(modelBuilder);
        }

        
       
    }
}
