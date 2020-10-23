namespace DigitalMind.YoYoApp.Domain.Models
{
    public class Shuttle
    {

        public int AccumulatedShuttleDistance { get; set; }
        public int SpeedLevel { get; set; }
        public int ShuttleNo { get; set; }
        public float Speed { get; set; }
        public float LevelTime { get; set; }
        public string CommulativeTime { get; set; }
        public string StartTime { get; set; }
        public float ApproxVo2Max { get; set; }
    }
}
