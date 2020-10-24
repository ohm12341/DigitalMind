using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;

namespace DigitalMind.YoYoApp.Application.Interfaces
{
    public interface IAthleteShuttleServices
    {

        /// <summary>
        /// Get all shuttles in the test
        /// </summary>
        /// <returns>List of all shuttles</returns>
        List<Shuttle> GetAllShuttles();


        /// <summary>
        /// Add a lsit of shuttles to data base
        /// </summary>
        /// <param name="shuttles"> list of shuttles</param>
        void AddAllShuttles(List<Shuttle> shuttles);
    }
}
