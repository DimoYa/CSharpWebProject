using Moq;
using MyResourcePlanning.Models;
using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Services.Data.Calendar;
using MyResourcePlanning.Services.Data.User;
using MyResourcePlanning.Tests.Common;
using MyResourcePlanning.Web.BindingModels.Calendar;
using MyResourcePlanning.Web.ViewModels.Calendar;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResourcePlanning.Tests.Service
{
    [TestFixture]
    public class CalendarServiceTests
    {
        private Mock<IUserService> mockedUserService;
        private ICalendarService calendarService;
        private List<Calendar> dummyCalendar;
        private List<UserCalendar> dummyUsercalendar;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();

            this.mockedUserService = new Mock<IUserService>();
            this.calendarService = new CalendarService(context, mockedUserService.Object);

            this.dummyCalendar = DummyData.GetDummyCalendarDays();
            this.dummyUsercalendar = DummyData.GetDummyUserCalendars();

            context.AddRange(dummyCalendar);
            context.AddRange(dummyUsercalendar);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task CreateCalendarPeriod_WithNonExistingDays_ShouldCreateDays()
        {
            var mockedModel = new CalendarCreatePeriodBindingModel()
            {
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(10),
                IspublicHoliday = false,
            };

            var actualResults = await this.calendarService.CreatePeriod(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task EditCalendarPeriod_ShouldChangeIsPublicHoliday()
        {
            var calendarId = "3";

            var mockedModel = new CalendarEditPeriodBindingModel()
            {
                IspublicHoliday = false,
            };

            await this.calendarService.EditPeriod(mockedModel, calendarId);

            var actualResult = this.dummyCalendar
                .SingleOrDefault(c => c.Id == calendarId);

            Assert.IsFalse(actualResult.IsPublicHoliday);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task DeleteCalendarPeriod_ShouldDeleteTheRecord()
        {
            var calendarId = "4";

            var actualResult = await this.calendarService
                .DeletePeriod(calendarId);

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task CheckIfAbsenceExistOrIsPublicHoliday_WithExistingDay_ShouldReturnTrue()
        {
            var startDay = DateTime.Now;
            var endDay = DateTime.Now.AddDays(2);

            var actualResult = await this.calendarService
                .CheckIfAbsenceExistOrIsPublicHoliday(startDay, endDay);

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task CheckIfAbsenceExistOrIsPublicHoliday_WithPublicHoliday_ShouldReturnFalse()
        {
            var startDay = DateTime.Now.AddDays(3);
            var endDay = DateTime.Now.AddDays(5);

            var actualResult = await this.calendarService
                .CheckIfAbsenceExistOrIsPublicHoliday(startDay, endDay);

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task CheckIfAbsenceExistOrIsPublicHoliday_WithoutPublicHolidayOrExistingDays_ShouldReturnTrue()
        {
            var startDay = DateTime.Now.AddDays(4);
            var endDay = DateTime.Now.AddDays(9);

            var actualResult = await this.calendarService
                .CheckIfAbsenceExistOrIsPublicHoliday(startDay, endDay);

            Assert.IsFalse(actualResult);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task MapPeriod_ShouldMapCorrectPeriod()
        {
            var calendarPeriodId = "1";

            var actualResult = await this.calendarService
                .MapPeriod<CalendarAllViewModel>(calendarPeriodId);

            var expectedResults = this.dummyCalendar
                .SingleOrDefault(c => c.Id == calendarPeriodId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Day.Equals(expectedResults.Day.ToString("dd-MM-yyyy")));
                Assert.That(actualResult.IspublicHoliday.Equals(expectedResults.IsPublicHoliday));
            });
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task CreateAbsence_ShouldCreateAbsenceDays()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
              .Returns(Task.FromResult(currentUserId));

            var mockedModel = new CalendarCreateAbsenceBindingModel()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                AbsenceType = UserCalendarAbsenceType.SickLeave,
            };

            var actualResults = await this.calendarService.CreateAbsence(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task DeleteAbsence_ShouldDeleteTheRecord()
        {
            var currentUserId = "123";
            var calendarId = "2";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
              .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.calendarService
                .DeleteAbsence(calendarId);

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task GetAllDays_ShouldReturnAllCalendarDays()
        {
            var actualResults = await this.calendarService
                .GetAllDays<CalendarAllViewModel>();

            var expectedResults = this.dummyCalendar;

            CollectionAssert.AreEqual(
                expectedResults.OrderBy(c => c.Id).Select(c => c.Id),
                actualResults.OrderBy(c => c.Id).Select(c => c.Id));
        }

        [Test]
        [Property("service", "CalendarService")]
        public async Task GetMyAbsenceDays_ShouldReturnAllCalendarDays()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
              .Returns(Task.FromResult(currentUserId));

            var actualResults = await this.calendarService
                .GetMyAbsenceDays<CalendarMyViewModel>();

            var expectedResults = this.dummyUsercalendar
                .Where(uc=> uc.UserId == currentUserId);

            CollectionAssert.AreEqual(
                expectedResults.OrderBy(c => c.CalendarId).Select(c => c.CalendarId),
                actualResults.OrderBy(c => c.CalendarId).Select(c => c.CalendarId));
        }
    }
}
