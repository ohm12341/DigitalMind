using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;

namespace DigitalMind.YoYoApp.Application.Providers
{
    public interface IStopWatchProvider
    {

        /// <summary>
        /// Get a stopwatch viewmodel for a given shuttle number and speedlevel
        /// </summary>
        /// <param name="shuttleNumber"></param>
        /// <param name="speedLevel"></param>
        /// <returns></returns>
        StopWatchViewModel GetStopWatchViewModel(int shuttleNumber, int speedLevel);

        /// <summary>
        /// Lsit of shuttles 
        /// </summary>
        List<Shuttle> Shuttles { get; set; }
    }
}
