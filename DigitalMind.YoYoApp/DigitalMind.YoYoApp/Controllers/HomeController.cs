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

            InitializeShuttleTable();
            InitializeAthleteTable();
        }

        public IActionResult Index()
        {

            var shuttles = _athleteShuttleServices.GetAllShuttles();
            var athletes = _athleteShuttleServices.GetAllAthletes();

            return View();
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


        private void InitializeShuttleTable()
        {
            List<Shuttle> sortlst;
            using (StreamReader r = new StreamReader(@"TestData\fitnessrating_beeptest.json"))
            {
                string json = r.ReadToEnd();
                List<Shuttle> items = JsonConvert.DeserializeObject<List<Shuttle>>(json);

                sortlst = items.OrderBy(x => x.ShuttleNo).ThenBy(x => x.SpeedLevel).ToList();

            }
            _athleteShuttleServices?.AddAllShuttles(sortlst);

        }

        private void InitializeAthleteTable()
        {

            var testdata = new List<Athlete>()
            {
                new Athlete()
                {

                    Name = "Usain Bolt"
                },
                new Athlete()
                {

                    Name = "Freddy Adu"
                },
                new Athlete()
                {

                    Name = "William Looby"
                },
                new Athlete()
                {

                    Name = "Charlie Davies"
                },
                new Athlete()
                {

                    Name = "Maurice Edu"
                },
                new Athlete()
                {

                    Name = "Bart McGhee"
                },
                new Athlete()
                {

                    Name = "Jozy Altidore"
                },
                new Athlete()
                {

                    Name = "Steve Cherundolo"
                },
                new Athlete()
                {

                    Name = "DaMarcus Beasley"
                },
                new Athlete()
                {

                    Name = "Jay DeMerit"
                },
                new Athlete()
                {

                    Name = "Oguchi Onyewu"
                },
                new Athlete()
                {

                    Name = "Adelino Gonsalves"
                },
                new Athlete()
                {

                    Name = "Aldo Donelli"
                },
                new Athlete()
                {

                    Name = "Thomas Florie"
                },
                new Athlete()
                {

                    Name = "Michael Bradley"
                },
                new Athlete()
                {

                    Name = "Fernando Clavijo"
                },
                new Athlete()
                {

                    Name = "Rick Davis"
                },
                new Athlete()
                {

                    Name = "Roy Lassiter"
                },
                new Athlete()
                {

                    Name = "Tab Ramos"
                }

            };
            _athleteShuttleServices?.AddAllAthletes(testdata);
        }
    }
}
