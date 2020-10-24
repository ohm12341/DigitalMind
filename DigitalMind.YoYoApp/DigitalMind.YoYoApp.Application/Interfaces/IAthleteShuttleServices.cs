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
        /// Get all Athletes in the test
        /// </summary>
        /// <returns>List of all Athletes</returns>
        List<Athlete> GetAllAthletes();


        /// <summary>
        /// Add a list of shuttles to data base
        /// </summary>
        /// <param name="shuttles"> list of shuttles</param>
        void AddAllShuttles(List<Shuttle> shuttles);


        /// <summary>
        /// Add a lsit of athletes to data base
        /// </summary>
        /// <param name="athletes"> list of athletes</param>
        void AddAllAthletes(List<Athlete> athletes);
    }
}
