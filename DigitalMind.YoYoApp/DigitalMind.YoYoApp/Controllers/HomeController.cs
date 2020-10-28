using DigitalMind.YoYoApp.Application.Interfaces;
using DigitalMind.YoYoApp.Application.Providers;
using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Models;
using DigitalMind.YoYoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DigitalMind.YoYoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IStopWatchViewModel stopWatchViewModel;
      
        private readonly IAthleteShuttleServices athleteShuttleServices;

        private readonly IStopWatchProvider stopWatchProvider;
        private readonly IAthleteListProvider athleteListProvider;

        public HomeController(ILogger<HomeController> logger,
            IStopWatchViewModel stopWatchViewModel,
            IAthleteShuttleServices athleteShuttleServices,
            IStopWatchProvider stopWatchProvider,
            IAthleteListProvider  athleteListProvider)
        {
            _logger = logger;
            this.stopWatchViewModel = stopWatchViewModel;
            this.athleteShuttleServices = athleteShuttleServices;
            this.stopWatchProvider = stopWatchProvider;
            this.athleteListProvider = athleteListProvider;
            this.stopWatchProvider.Shuttles = athleteShuttleServices.GetAllShuttles();
            this.athleteListProvider.SetAthlete(athleteShuttleServices.GetAllAthletes());
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetStopWatchViewModel(int shuttleNumber, int speedLevel)
        {
            var viewmodel = stopWatchProvider.GetStopWatchViewModel(shuttleNumber, speedLevel);
            return Json(viewmodel);
        }

        public IActionResult GetUpdatedAthleteViewModel(int athleteId,string testresult)
        {
            var viewmodel = athleteListProvider.UpdateAndReturnNewAtheleViewModel(athleteId, testresult);
            return Json(viewmodel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       
    }
}
