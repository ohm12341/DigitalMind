﻿using DigitalMind.YoYoApp.Application.Providers;
using DigitalMind.YoYoApp.Application.Services;
using DigitalMind.YoYoApp.Domain.Interfaces;
using DigitalMind.YoYoApp.Domain.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DigitalMind.YoYo.Application.Tests
{
    public class AthleteListProviderTest
    {

        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndGetUpdatedAtheleViewModelCalled_ANonEmptyAthleteViewModelIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();
          
            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            var viewModel = athleteListProvider.GetUpdatedAtheleViewModel(1, "start", 8, 20);

            Assert.NotNull(viewModel);

            Assert.Equal(GetTestAthleteData().Count(), viewModel.Athletes.Count);
        }

        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndGetUpdatedAtheleViewModelCalledWithStatusWarn_ANonEmptyAthleteViewModelIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();

            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            var viewModel = athleteListProvider.GetUpdatedAtheleViewModel(1, "warn", 8, 20);

            Assert.NotNull(viewModel);

            Assert.True(viewModel.Athletes.Where(x=>x.ShuttleState.Equals("warn")).Count()>0);
        }

        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndGetUpdatedAtheleViewModelCalledWithStatusStop_ANonEmptyAthleteViewModelIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();

            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            athleteListProvider.SetShuttles(athleteShuttleServices.GetAllShuttles());

            var viewModel = athleteListProvider.GetUpdatedAtheleViewModel(1, "stop", 8, 20);

            Assert.NotNull(viewModel);

            Assert.True(viewModel.Athletes.Where(x => x.ShuttleState.Equals("stop")).Count() > 0);
        }


        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndGetUpdatedAtheleViewModelCalledWithStatusStop_ANonEmptyAthleteViewModelWithFinishedShuttleIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();

            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            athleteListProvider.SetShuttles(athleteShuttleServices.GetAllShuttles());

            var viewModel = athleteListProvider.GetUpdatedAtheleViewModel(1, "stop", 8, 20);

            Assert.NotNull(viewModel);

            Assert.True(viewModel.Athletes.FirstOrDefault(x => x.ShuttleState.Equals("stop")).FinishedShuttles.Count() > 0);
        }


        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndUpdateTestResultForAnAthleteCalledWithStatusWarn_ANonEmptyAthleteViewModelIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();

            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            athleteListProvider.SetShuttles(athleteShuttleServices.GetAllShuttles());


            var viewModel = athleteListProvider.UpdateTestResultForAnAthlete(1, "warn", 8, 20);

            Assert.NotNull(viewModel);

            Assert.True(viewModel.Athletes.Where(x => x.ShuttleState.Equals("warn")).Count() > 0);
        }

        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndUpdateTestResultForAnAthleteCalledWithStatusStop_ANonEmptyAthleteViewModelIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();

            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            athleteListProvider.SetShuttles(athleteShuttleServices.GetAllShuttles());

            var viewModel = athleteListProvider.UpdateTestResultForAnAthlete(1, "stop", 8, 20);

            Assert.NotNull(viewModel);

            Assert.True(viewModel.Athletes.Where(x => x.ShuttleState.Equals("stop")).Count() > 0);
        }


        [Fact]
        public void WhenAthleteListProvider_SetAthletesAndUpdateTestResultForAnAthleteWithStatusStop_ANonEmptyAthleteViewModelWithFinishedShuttleIsReturned()
        {
            Mock<IRepository<Shuttle>> shuttleRepository = new Mock<IRepository<Shuttle>>();
            Mock<IRepository<Athlete>> athleteRepository = new Mock<IRepository<Athlete>>();

            shuttleRepository.Setup(x => x.GetAll()).Returns(GetTestShuttleData());

            athleteRepository.Setup(x => x.GetAll()).Returns(GetTestAthleteData());
            AthleteShuttleServices athleteShuttleServices = new AthleteShuttleServices(
                                                         shuttleRepository.Object,
                                                         athleteRepository.Object);
            AthleteListProvider athleteListProvider = new AthleteListProvider();

            athleteListProvider.SetAthletes(athleteShuttleServices.GetAllAthletes());

            athleteListProvider.SetShuttles(athleteShuttleServices.GetAllShuttles());

            var viewModel = athleteListProvider.UpdateTestResultForAnAthlete(1, "stop", 8, 20);

            Assert.NotNull(viewModel);

            Assert.NotNull(viewModel.Athletes.FirstOrDefault(x => x.ShuttleState.Equals("stop")).CurrentShuttle);
        }


        private IQueryable<Athlete> GetTestAthleteData()
        {
            return new List<Athlete>()
            {

                new Athlete()
                {
                     Id=1,
                    Name = "Usain Bolt",
                     ShuttleState="Start"
                },
                new Athlete()
                {
                      Id=2,
                    Name = "William Looby",
                     ShuttleState="Start"
                },
                new Athlete()
                {
                     Id=3,
                    Name = "Charlie Davies",
                     ShuttleState="Start"
                },
                new Athlete()
                {
                     Id=4,
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

            }.AsQueryable();
        }

        private IQueryable<Shuttle> GetTestShuttleData()
        {
            return new List<Shuttle>()
            {
                new Shuttle()
                {
 AccumulatedShuttleDistance= 2680,
    SpeedLevel= 20,
    ShuttleNo= 8,
    Speed= 17.5f,
    LevelTime= 8.2f,
    CommulativeTime= "21:38",
    StartTime= "21:20",
    ApproxVo2Max= 58.91f
                },
                 new Shuttle()
                {
 AccumulatedShuttleDistance= 2681,
    SpeedLevel= 21,
    ShuttleNo= 9,
    Speed= 18.5f,
    LevelTime= 9.2f,
    CommulativeTime= "21:38",
    StartTime= "21:20",
    ApproxVo2Max= 58.91f
                },
                  new Shuttle()
                {
 AccumulatedShuttleDistance= 2682,
    SpeedLevel= 22,
    ShuttleNo= 10,
    Speed= 17.5f,
    LevelTime= 8.2f,
    CommulativeTime= "21:38",
    StartTime= "21:20",
    ApproxVo2Max= 58.91f
                }
            }.AsQueryable();
        }
    }


}
