using DigitalMind.YoYoApp.Application.Interfaces;
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

        private readonly IAthleteShuttleServices _athleteShuttleServices;

        public HomeController(ILogger<HomeController> logger,
            IAthleteShuttleServices athleteShuttleServices)
        {
            _logger = logger;
            _athleteShuttleServices = athleteShuttleServices;
        }

        public IActionResult Index()
        {
            _athleteShuttleServices.AddAllShuttles(LoadJson());
            var data = _athleteShuttleServices.GetAllShuttles();

            return View(data);
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


        private List<Shuttle> LoadJson()
        {
            List<Shuttle> sortlst;
            using (StreamReader r = new StreamReader(@"TestData\fitnessrating_beeptest.json"))
            {
                string json = r.ReadToEnd();
                List<Shuttle> items = JsonConvert.DeserializeObject<List<Shuttle>>(json);

                for (int i = 0; i < items.Count; i++)
                {
                    items[i].Id = i + 1;
                }
                sortlst = items.OrderBy(x => x.ShuttleNo).ThenBy(x => x.SpeedLevel).ToList();

            }

            return sortlst;
        }
    }
}
