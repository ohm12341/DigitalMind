using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;

namespace DigitalMind.YoYoApp.Application.Providers
{
    public interface IAthleteListProvider
    {

        void SetShuttles(List<Shuttle> shuttles);

        void SetAthletes(List<Athlete> Athletes);

        AthleteViewModel GetUpdatedAtheleViewModel(int athleteId, 
            string testresult,
            int shuttlenumber,
           int shuttlespeedlevel);

        AthleteViewModel UpdateTestResultForAnAthlete(int athleteId,
          string testresult,
          int shuttlenumber,
         int shuttlespeedlevel);


    }
}
