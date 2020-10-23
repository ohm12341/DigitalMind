using DigitalMind.YoYoApp.Domain.Common.Enum;

namespace DigitalMind.YoYoApp.Domain.Models
{
    public class Athlete
    {

        public Shuttle FinalShuttle { get; set; }
        public Shuttle CurrentShuttle { get; set; }
        public AthleteTestState ShuttleState { get; set; }
    }
}
