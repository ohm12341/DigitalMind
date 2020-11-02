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


        public void SetAthletes(List<Athlete> athletes)
        {
            if (!isAthleteInitialized)
            {
                _athletes = athletes;
                isAthleteInitialized = true;
            }


        }

        public void SetShuttles(List<Shuttle> shuttles)
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
                    if (athlete.ShuttleState.Equals("stop"))
                    {
                        var indexofcurrentshuttle = _shuttles.IndexOf(_shuttles.FirstOrDefault(x => x.ShuttleNo == shuttlenumber && x.SpeedLevel == shuttlespeedlevel));

                        athlete.FinishedShuttles.AddRange(_shuttles.Where(x => x.ShuttleNo == shuttlenumber && x.SpeedLevel <= shuttlespeedlevel).ToList());

                        athlete.FinishedShuttles.AddRange(_shuttles.Where(x => x.ShuttleNo < shuttlenumber).ToList());

                        if (athlete.CurrentShuttle == null)
                            if (indexofcurrentshuttle >= 1)
                            {
                                athlete.CurrentShuttle = _shuttles[indexofcurrentshuttle - 1];
                            }
                            else
                            {
                                athlete.CurrentShuttle = new Shuttle();
                            }

                    }

                }
            }

            AthleteViewModel athleteViewModel = new AthleteViewModel()
            {
                Athletes = _athletes,
            };

            return athleteViewModel;
        }

        public AthleteViewModel UpdateTestResultForAnAthlete(int athleteId,
            string testresult,
            int shuttlenumber,
            int shuttlespeedlevel)
        {
            foreach (var athlete in _athletes)
            {
                if (athlete.Id.Equals(athleteId))
                {
                    var indexofcurrentshuttle = _shuttles.IndexOf(_shuttles.FirstOrDefault(x => x.ShuttleNo == shuttlenumber && x.SpeedLevel == shuttlespeedlevel));
                    athlete.ShuttleState = testresult;
                 
                    if (indexofcurrentshuttle >= 0)
                        athlete.CurrentShuttle = _shuttles[indexofcurrentshuttle];
                }
            }

            AthleteViewModel athleteViewModel = new AthleteViewModel()
            {
                Athletes = _athletes,
            };

            return athleteViewModel;
        }

        public void SaveYoYoTestResult()
        {
            this._shuttles = new List<Shuttle>();
            isAthleteInitialized = false;
            isShuttleInitialized = false;
        }
    }
}
