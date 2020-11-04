using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DigitalMind.YoYoApp.Application.Providers
{
    public class StopWatchProvider : IStopWatchProvider
    {
        public List<Shuttle> Shuttles { get; set; }

        public StopWatchViewModel GetStopWatchViewModel(int shuttleNumber, int speedLevel)
        {
            var shuttle = Shuttles.FirstOrDefault(x => x.ShuttleNo.Equals(shuttleNumber) && x.SpeedLevel.Equals(speedLevel));
            var currentshuttleindex = Shuttles.IndexOf(shuttle);
            if (currentshuttleindex >= 0)
            {
                if(currentshuttleindex+1 == Shuttles.Count - 1)
                {
                    return new StopWatchViewModel()
                    {
                        ShuttleNo = Shuttles[currentshuttleindex + 1].ShuttleNo,
                        SpeedLevel = Shuttles[currentshuttleindex + 1].SpeedLevel,
                        Speed = Shuttles[currentshuttleindex + 1].Speed,
                        LevelTime = Shuttles[currentshuttleindex + 1].LevelTime,
                        IsFinalShuttle = true,
                        TotalTimeForTest = Shuttles.Sum(x => x.LevelTime),
                        NextShuttle = 0,
                        TotalDistance = GetTotalDistance(shuttleNumber, speedLevel),
                        TotalTime = GetTotalTime(shuttleNumber, speedLevel)
                    };
                }
                return new StopWatchViewModel()
                {
                    ShuttleNo = Shuttles[currentshuttleindex + 1].ShuttleNo,
                    SpeedLevel = Shuttles[currentshuttleindex + 1].SpeedLevel,
                    Speed = Shuttles[currentshuttleindex + 1].Speed,
                    LevelTime = Shuttles[currentshuttleindex + 1].LevelTime,
                    IsFinalShuttle = false,
                    TotalTimeForTest = Shuttles.Sum(x => x.LevelTime),
                    NextShuttle = (currentshuttleindex + 2 < Shuttles.Count) ? Shuttles[currentshuttleindex + 2].Speed : 0,
                    TotalDistance = GetTotalDistance(shuttleNumber, speedLevel),
                    TotalTime = GetTotalTime(shuttleNumber, speedLevel)
                };

            }
            else
            {
                var firstshuttle = Shuttles.FirstOrDefault();
                return new StopWatchViewModel()
                {
                    IsFinalShuttle = false,
                    ShuttleNo = firstshuttle.ShuttleNo,
                    SpeedLevel = firstshuttle.SpeedLevel,
                    Speed = firstshuttle.Speed,
                    TotalTimeForTest = Shuttles.Sum(x => x.LevelTime),
                    LevelTime = firstshuttle.LevelTime,
                    NextShuttle = (Shuttles.Count >=2) ? Shuttles[1].Speed : 0,

                };
            }


        }

        public void SaveTestShuttleState()
        {
            this.Shuttles = new List<Shuttle>();
        }

        private float GetTotalDistance(int shuttlenumber,int shuttlespeedlevel)
        {
            float totDist = 0;

            totDist+= Shuttles.Where(x => x.ShuttleNo == shuttlenumber && x.SpeedLevel <= shuttlespeedlevel).Sum(x => x.AccumulatedShuttleDistance);
            totDist += Shuttles.Where(x => x.ShuttleNo < shuttlenumber && x.SpeedLevel <= shuttlespeedlevel).ToList().Sum(x => x.AccumulatedShuttleDistance);

            return totDist;
        }

        private float GetTotalTime(int shuttlenumber, int shuttlespeedlevel)
        {
            float totDist = 0;

            totDist += Shuttles.Where(x => x.ShuttleNo == shuttlenumber && x.SpeedLevel <= shuttlespeedlevel).Sum(x => x.LevelTime);
            totDist += Shuttles.Where(x => x.ShuttleNo < shuttlenumber && x.SpeedLevel <= shuttlespeedlevel).ToList().Sum(x => x.LevelTime);

            return totDist;
        }
    }
}
