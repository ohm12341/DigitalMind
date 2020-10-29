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
            var currentshuttleindex = Shuttles.IndexOf(shuttle)+1;
            if (currentshuttleindex > 0 && Shuttles.Count > currentshuttleindex)
            {

               

                return new StopWatchViewModel()
                {

                    ShuttleNo = Shuttles[currentshuttleindex].ShuttleNo,
                    SpeedLevel = Shuttles[currentshuttleindex].SpeedLevel,
                    Speed = Shuttles[currentshuttleindex].Speed,
                    LevelTime= Shuttles[currentshuttleindex].LevelTime,
                    NextShuttle = (currentshuttleindex + 1 <= Shuttles.Count) ? Shuttles[currentshuttleindex + 1].Speed : 0,
                    TotalDistance = Shuttles.Where(x => x.ShuttleNo <= shuttleNumber && x.SpeedLevel <= speedLevel).ToList().Sum(x => x.AccumulatedShuttleDistance),
                    TotalTime = Shuttles.Where(x => x.ShuttleNo <= shuttleNumber && x.SpeedLevel <= speedLevel).ToList().Sum(x => x.LevelTime),
                };

            }
            else
            {

                var firstshuttle = Shuttles.FirstOrDefault();
                return new StopWatchViewModel()
                {

                    ShuttleNo = firstshuttle.ShuttleNo,
                    SpeedLevel = firstshuttle.SpeedLevel,
                    Speed = firstshuttle.Speed,
                    LevelTime = firstshuttle.LevelTime,
                    NextShuttle = (Shuttles.Count>=1) ? Shuttles[1].Speed : 0,

                };
            }

        }
    }
}
