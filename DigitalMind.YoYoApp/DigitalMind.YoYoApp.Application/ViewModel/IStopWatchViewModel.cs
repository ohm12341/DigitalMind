namespace DigitalMind.YoYoApp.Application.ViewModel
{
    public interface IStopWatchViewModel
    {
        public int SpeedLevel { get; set; }
        public int ShuttleNo { get; set; }
        
        bool IsFinalShuttle { get; set; }
        public float LevelTime { get; set; }
        public float Speed { get; set; }

        float TotalDistance { get; set; }

        float TotalTime { get; set; }

        float NextShuttle { get; set; }

    }
}
