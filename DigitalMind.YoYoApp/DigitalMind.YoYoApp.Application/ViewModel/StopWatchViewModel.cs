namespace DigitalMind.YoYoApp.Application.ViewModel
{
    public class StopWatchViewModel : IStopWatchViewModel
    {
        public int SpeedLevel { get; set; }
        public int ShuttleNo { get; set; }
        public float Speed { get; set; }

        public float TotalDistance { get; set; }

        public float TotalTime { get; set; }

        public float NextShuttle { get; set; }
        public float LevelTime { get; set; }
    }
}
