using DigitalMind.YoYoApp.Domain.Common.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DigitalMind.YoYoApp.Domain.Models
{
    public class Athlete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Shuttle> FinishedShuttles { get; set; } = new List<Shuttle>();

        public Shuttle CurrentShuttle { get; set; }
        public string ShuttleState { get; set; }
    }
}
