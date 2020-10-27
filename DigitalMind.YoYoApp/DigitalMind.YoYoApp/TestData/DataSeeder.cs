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
            context.AddRange(testdata);
            context.SaveChanges();

        }
    }
}
