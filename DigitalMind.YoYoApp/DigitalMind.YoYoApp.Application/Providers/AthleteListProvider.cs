using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DigitalMind.YoYoApp.Application.Providers
{
    public class AthleteListProvider : IAthleteListProvider
    {
        List<Athlete> _athletes { get; set; }

        List<Shuttle> _shuttles { get; set; }

        bool isAthleteInitialized;

        bool isShuttleInitialized;


        public void SetAthletes(List<Athlete>  athletes)
        {
            if (!isAthleteInitialized)
            {
                _athletes = athletes;
                isAthleteInitialized = true;
            }


        }

        public void SetShuttles(List<Shuttle>  shuttles)
        {
            if (!isShuttleInitialized)
            {
                _shuttles = shuttles;
                isShuttleInitialized = true;
            }


        }

        public AthleteViewModel GetUpdatedAtheleViewModel(int athleteId,
            string testresult,
            int shuttlenumber,
           int shuttlespeedlevel)
        {

            foreach (var athlete in _athletes)
            {
                if (athlete.Id.Equals(athleteId))
                {
                    athlete.ShuttleState = testresult;
                    athlete.FinishedShuttles = _shuttles.Where(x => x.ShuttleNo <= shuttlenumber && x.SpeedLevel <= shuttlespeedlevel).ToList();
                }
            }

            AthleteViewModel athleteViewModel = new AthleteViewModel()
            {
                Athletes = _athletes,
            };

            return athleteViewModel;
        }

    }
}
