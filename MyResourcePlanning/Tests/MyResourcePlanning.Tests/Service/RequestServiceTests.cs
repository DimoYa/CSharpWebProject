using Moq;
using MyResourcePlanning.Models;
using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Services.Data.Project;
using MyResourcePlanning.Services.Data.Request;
using MyResourcePlanning.Services.Data.User;
using MyResourcePlanning.Tests.Common;
using MyResourcePlanning.Web.BindingModels.Request;
using MyResourcePlanning.Web.ViewModels.Request;
using MyResourcePlanning.Web.ViewModels.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResourcePlanning.Tests.Service
{
    [TestFixture]
    public class RequestServiceTests
    {
        private Mock<IUserService> mockedUserService;
        private IProjectService projectService;
        private IRequestService requestService;
        private List<Project> dummyProjects;
        private List<Request> dummyRequests;
        private List<User> dummyUsers;
        private List<Calendar> dummyCalendars;
        private List<Skill> dummySkills;
        private List<Training> dummyTrainings;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.projectService = new ProjectService(context);
            this.mockedUserService = new Mock<IUserService>();
            this.requestService = new RequestService(context , mockedUserService.Object,
                projectService);

            this.dummyUsers = DummyData.GetDummyUsers();
            this.dummyProjects = DummyData.GetDummyProjects();
            this.dummyRequests = DummyData.GetDummyRequests();
            this.dummyCalendars = DummyData.GetDummyCalendarDays();
            this.dummySkills = DummyData.GetDummySkills();
            this.dummyTrainings = DummyData.GetDummyTrainings();

            context.AddRange(dummyUsers);
            context.AddRange(dummyProjects);
            context.AddRange(dummyRequests);
            context.AddRange(dummySkills);
            context.AddRange(dummyTrainings);
            context.AddRange(dummyCalendars);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task CreateRequest_WithDummyData_ShouldReturnCorrectResults()
        {
            var mockedModel = new RequestCreateBindingModel()
            {
                StartDate = DateTime.Now.AddDays(-5),
                EndDate = DateTime.Now.AddDays(15),
                Resource = "FirstName LastName",
                Project = $"Project1 (10.00) {DateTime.Now.AddDays(5)} -> {DateTime.Now.AddDays(60)}",
                WorkingHours = 100,
            };

            var actualResults = await this.requestService.Create(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task EditRequest_WithDummyData_ShouldReturnCorrectResults()
        {
            var requestId = "1";
            var newStartDate = DateTime.Now;
            var newEndDate = DateTime.Now.AddDays(18);
            var hours = 5;

            var mockedModel = new RequestEditBindingModel()
            {
                StartDate = newStartDate,
                EndDate = newEndDate,
                WorkingHours = hours,
            };

            await this.requestService.Edit(mockedModel, requestId);

            var actualResult = this.dummyRequests
                .SingleOrDefault(r => r.Id == requestId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.StartDate.Equals(newStartDate));
                Assert.That(actualResult.EndDate.Equals(newEndDate));
                Assert.That(actualResult.WorkingHours.Equals(hours));

            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task DeleteRequest_ShouldSetIsDeletedFlag()
        {
            var requestId = "3";

            await this.requestService.Delete(requestId);

            var actualResult = this.dummyRequests
                .SingleOrDefault(r => r.Id == requestId)
                .IsDeleted;

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task ApproveRequest_ShouldReSetApprovedState()
        {
            var requestId = "2";
            var comment = "approved";
            
            await this.requestService.Approve(requestId, comment);

            var actualResult = this.dummyRequests
                .SingleOrDefault(r => r.Id == requestId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status.Equals(RequestStatus.Booked));
                Assert.That(actualResult.Comment.Contains(comment));
            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task RejectRequest_ShouldReSetRejectedState()
        {
            var requestId = "2";
            var comment = "rejected";

            await this.requestService.Reject(requestId, comment);

            var actualResult = this.dummyRequests
                .SingleOrDefault(r => r.Id == requestId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status.Equals(RequestStatus.Rejected));
                Assert.That(actualResult.Comment.Contains(comment));
            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task ReturnRequest_ShouldReSetReturnedState()
        {
            var requestId = "3";
            var comment = "returned";

            await this.requestService.Return(requestId, comment);

            var actualResult = this.dummyRequests
                .SingleOrDefault(r => r.Id == requestId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status.Equals(RequestStatus.Returned));
                Assert.That(actualResult.Comment.Contains(comment));
            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task GetEmployeeDetails_WithDummyData_ShouldReturnCorrectResults()
        {
            var userId = "129";

            this.mockedUserService.Setup(x => x.GetUserByName("Ivan", "Ivanov"))
            .Returns(Task.FromResult(this.dummyUsers
            .SingleOrDefault(u => u.Id == userId)));

            var model = new RequestUserDetailsBaseModel()
            {
                BindingnModel = new RequestCreateUserDetailsBindingModel()
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(5),
                    Resource = "Ivan Ivanov"
                }
            };

            var actualResult = await this.requestService
                .GetEmployeeDetails<RequestUserDetailsBaseModel>(model);

            var expectedResults = this.dummyUsers
                .SingleOrDefault(u => u.Id == userId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.ViewModel.FreeHours.Equals("16.00"));

                CollectionAssert.AreEqual(
                    actualResult.ViewModel.Skills.Select(s => s.Skill.Id),
                    expectedResults.Skills.Select(s => s.Skill.Id));

                CollectionAssert.AreEqual(
                    actualResult.ViewModel.Trainings.Select(t => t.Training.Id),
                    expectedResults.Trainings.Select(t => t.Training.Id));
            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task GetRequestCommentsById_ShouldReturnRequestComments()
        {
            var requestId = "6";

            var actualResult = await this.requestService
                .GetRequestCommentsById(requestId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult[0].Equals("Comment1"));
                Assert.That(actualResult[1].Equals("Comment2"));
                Assert.That(actualResult[2].Equals("Comment3"));
            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task MapRequest_ShouldMapTheRequestDataToModel()
        {
            var requestId = "6";

            var actualResult = await this.requestService
                .MapRequest<RequestAllViewModel>(requestId);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<RequestAllViewModel>(actualResult);
                Assert.That(actualResult.Id.Equals(requestId));
                Assert.That(actualResult.Project.Equals("Project1"));
                Assert.That(actualResult.Resource.Equals("User User"));
            });
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task GetAllPlannerRequests_WithDummyData_ShouldReturnPlannersRequests()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.requestService
                .GetAllPlannerRequests<RequestAllViewModel>();

            var expectedResult = this.dummyRequests
                .Where(r => r.CreatedBy == currentUserId)
                .ToList();

            CollectionAssert.AreEqual(
                actualResult.Select(r=> r.Id),
                expectedResult.Select(r => r.Id));
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task GetAllApproverRequests_WithDummyData_ShouldReturnApproversRequests()
        {
            var currentUserId = "250";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.requestService
                .GetAllApproverRequests<RequestAllViewModel>();

            var expectedResult = this.dummyRequests
                .Where(r => r.User.ApproverId == currentUserId)
                .ToList();

            CollectionAssert.AreEqual(
                actualResult.Select(r => r.Id),
                expectedResult.Select(r => r.Id));
        }

        [Test]
        [Property("service", "RequestService")]
        public async Task GetAllResourceRequests_WithDummyData_ShouldReturnApproversRequests()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.requestService
                .GetAllResourceRequests<RequestAllViewModel>();

            var expectedResult = this.dummyRequests
                .Where(r => r.User.Id == currentUserId)
                .Where(r => r.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(
                actualResult.Select(r => r.Id),
                expectedResult.Select(r => r.Id));
        }
    }
}
