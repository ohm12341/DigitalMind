using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using System.Collections.Generic;

namespace DigitalMind.YoYoApp.Application.Providers
{
    public interface IAthleteListProvider
    {
        

        void SetAthlete(List<Athlete> Athletes);

        AthleteViewModel UpdateAndReturnNewAtheleViewModel(int athleteId, string testresult);

        
    }
}
