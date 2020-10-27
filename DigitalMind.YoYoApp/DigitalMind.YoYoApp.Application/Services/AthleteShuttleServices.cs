using DigitalMind.YoYoApp.Application.Interfaces;
using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Interfaces;
using DigitalMind.YoYoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalMind.YoYoApp.Application.Services
{
    public class AthleteShuttleServices : IAthleteShuttleServices
    {
        private readonly IRepository<Shuttle> _shuttleRepository;

        private readonly IRepository<Athlete> _athleteRepository;
      
        
        public AthleteShuttleServices(
            IRepository<Shuttle> shuttleRepository,
            IRepository<Athlete> athleteRepository)
        {
            _shuttleRepository = shuttleRepository;
            _athleteRepository = athleteRepository;

        }

        public void AddAllAthletes(List<Athlete> athletes)
        {
            try
            {
                foreach (var athlete in athletes)
                {
                    _athleteRepository.Add(athlete);
                }

                _athleteRepository.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddAllShuttles(List<Shuttle> shuttles)
        {
            try
            {
                foreach (var shuttle in shuttles)
                {
                    _shuttleRepository.Add(shuttle);
                }

                _shuttleRepository.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Athlete> GetAllAthletes()
        {
            try
            {
                return _athleteRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Shuttle> GetAllShuttles()
        {
            try
            {
               
                return _shuttleRepository.GetAll().ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

     
    }
}
