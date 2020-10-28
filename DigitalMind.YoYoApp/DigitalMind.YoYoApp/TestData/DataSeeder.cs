using DigitalMind.YoYoApp.Domain.Models;
using DigitalMind.YoYoApp.Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalMind.YoYoApp.TestData
{
    public static class DataSeeder
    {

        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<YoYoTestDbContext>();

                InitializeShuttleTable(context);
                InitializeAthleteTable(context);
            }
            return host;
        }
        private static void InitializeShuttleTable(DbContext context)
        {
            List<Shuttle> sortlst;
            using (StreamReader r = new StreamReader(@"TestData\fitnessrating_beeptest.json"))
            {
                string json = r.ReadToEnd();
                List<Shuttle> items = JsonConvert.DeserializeObject<List<Shuttle>>(json);

                sortlst = items.OrderBy(x => x.ShuttleNo).ThenBy(x => x.SpeedLevel).ToList();

            }
            context.AddRange(sortlst);
            context.SaveChanges();

        }

        private static void InitializeAthleteTable(DbContext context)
        {

            var testdata = new List<Athlete>()
            {
                new Athlete()
                {

                    Name = "Usain Bolt",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Usain Bolt",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "William Looby",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Charlie Davies",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Maurice Edu",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Bart McGhee",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Jozy Altidore",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Steve Cherundolo",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "DaMarcus Beasley",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Jay DeMerit",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Oguchi Onyewu",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Adelino Gonsalves",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Aldo Donelli",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Thomas Florie",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Michael Bradley",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Fernando Clavijo",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Rick Davis",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Roy Lassiter",
                     ShuttleState="Start"
                },
                new Athlete()
                {

                    Name = "Tab Ramos",
                     ShuttleState="Start"
                }

            };
            context.AddRange(testdata);
            context.SaveChanges();

        }
    }
}
