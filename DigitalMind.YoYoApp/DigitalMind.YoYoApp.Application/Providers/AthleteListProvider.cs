using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;

namespace DigitalMind.YoYoApp.Application.Providers
{
    public class AthleteListProvider : IAthleteListProvider
    {
        List<Athlete> _athletes { get; set; }

        bool isAthleteInitialized;

        public void SetAthlete(List<Athlete> Athletes)
        {
            if (!isAthleteInitialized)
            {
                _athletes = Athletes;
                isAthleteInitialized = true;
            }


        }

        public AthleteViewModel UpdateAndReturnNewAtheleViewModel(int athleteId, string testresult)
        {

            foreach (var athlete in _athletes)
            {
                if (athlete.Id.Equals(athleteId))
                {
                    athlete.ShuttleState = testresult;

                }
            }

            AthleteViewModel athleteViewModel = new AthleteViewModel()
            {
                Athletes = _athletes
            };

            return athleteViewModel;
        }

    }
}
