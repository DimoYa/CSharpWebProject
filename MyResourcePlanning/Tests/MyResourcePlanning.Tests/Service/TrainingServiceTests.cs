using Moq;
using MyResourcePlanning.Models;
using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Services.Data.Training;
using MyResourcePlanning.Services.Data.User;
using MyResourcePlanning.Tests.Common;
using MyResourcePlanning.Web.BindingModels.Training;
using MyResourcePlanning.Web.ViewModels.Training;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResourcePlanning.Tests.Service
{
    [TestFixture]
    public class TrainingServiceTests
    {
        private ITrainingService trainingService;
        private Mock<IUserService> mockedUserService;
        private List<User> dummyUsers;
        private List<Training> dummyTrainings;
        private List<UserTraining> dummyUserTrainings;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.mockedUserService = new Mock<IUserService>();

            this.trainingService = new TrainingService(context, mockedUserService.Object);

            this.dummyUsers = DummyData.GetDummyUsers();
            this.dummyTrainings = DummyData.GetDummyTrainings();
            this.dummyUserTrainings = DummyData.GetDummyUserTrainings();

            context.AddRange(dummyUsers);
            context.AddRange(dummyTrainings);
            context.AddRange(dummyUserTrainings);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task CreateTraining_WithDummyData_ShouldReturnNewTraining()
        {
            var mockedModel = new TrainingCreateBindingModel()
            {
                Name = "NewTraining",
                Status = TrainingStatus.Active,
                Type = TrainingType.Mandatory,
                DueDate = DateTime.Now,
            };

            var actualResults = await this.trainingService.Create(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task EditTraining_WithDummyData_ShouldReturnCorrectResults()
        {
            var trainingId = "1";

            var mockedModel = new TrainingEditBindingModel()
            {
                Name = "EditTraining",
                Status = TrainingStatus.Inactive,
                Type = TrainingType.Optional,
            };

            var trainingForUpdate = this.dummyTrainings.SingleOrDefault(t => t.Id == trainingId);

            await this.trainingService.Edit(mockedModel, trainingId);

            var actualResult = this.dummyTrainings
                .SingleOrDefault(t => t.Id == trainingId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Name.Equals("EditTraining"));
                Assert.That(actualResult.Status.Equals(TrainingStatus.Inactive));
                Assert.That(actualResult.Type.Equals(TrainingType.Optional));
            });
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task DeleteTraining_ShouldSetIsDeletedFlag()
        {
            var trainingId = "3";

            await this.trainingService.Delete(trainingId);

            var actualResult = this.dummyTrainings
                .SingleOrDefault(t => t.Id == trainingId)
                .IsDeleted;

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task RequestTraining_ShouldReturnCorrectResults()
        {
            var trainingId = "3";
            var currentUserId = "127";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var mockedModel = new TrainingRequestBindingModel()
            {
               Name = "Training3",
            };

            var result = await this.trainingService.Request(trainingId, mockedModel);

            Assert.IsTrue(result);
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task AssignToUser_ShouldAssignUserToTraining()
        {
            var trainingId = "2";
            var userId = "123";

            this.mockedUserService.Setup(x => x.GetUserByName("FirstName", "LastName"))
            .Returns(Task.FromResult(this.dummyUsers.SingleOrDefault(u=> u.Id == userId)));

            var mockedModel = new TrainingAssignBindingModel()
            {
                Name = "Training2",
                Resource = "Ivan Ivanov",
            };

            var result = await this.trainingService
                .AssignToUser(trainingId, mockedModel);

            Assert.IsTrue(result);
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task ChangeUserTrainingStatus_ShouldReturnCorrectResults()
        {
            var trainingId = "1";
            var userId = "128";
            var newStatus = UserTrainingStatus.Rejected;

            var mockedModel = new TrainingStatusChangeBindingModel()
            {
                Status = newStatus,
            };

            await this.trainingService.ChangeUserTrainingStatus(mockedModel, trainingId, userId);

            var actualResult = this.dummyUserTrainings
                .SingleOrDefault(ut => ut.TrainingId == trainingId 
                && ut.UserId == userId);

            Assert.That(actualResult.Status.Equals(newStatus));
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task GetAllTrainings_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.trainingService
                .GetAllTrainings<TrainingAllViewModel>();

            var expectedResults = this.dummyTrainings
                .Where(s => s.IsDeleted == false)
                .OrderBy(s => s.Name)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(ut => ut.Id),
                expectedResults.Select(ut => ut.Id));
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task GetCurrentUserTrainings_WithDummyData_ShouldReturnCorrectResults()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResults = await this.trainingService.GetCurrentUserTrainings<TrainingUserViewModel>();

            var expectedResults = this.dummyUserTrainings
                 .Where(ut => ut.UserId == currentUserId)
                 .Where(ut => ut.Training.DueDate >= DateTime.Now)
                 .Where(ut => ut.Training.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(ut => ut.TrainingId),
                expectedResults.Select(ut => ut.TrainingId));
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task GetAllUsersTrainings_WithDummyData_ShouldReturnCorrectResults()
        {

            var actualResults = await this.trainingService.GetAllUsersTrainings<TrainingUserViewModel>();

            var expectedResults = this.dummyUserTrainings
                 .Where(ut => ut.Training.DueDate >= DateTime.Now)
                 .Where(ut => ut.Training.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(ut => ut.TrainingId),
                expectedResults.Select(ut => ut.TrainingId));
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task GetUserTrainingByIds_WithDummyData_ShouldReturnCorrectResults()
        {
            var trainingId = "1";
            var userId = "123";

            var actualResult = await this.trainingService
                .GetUserTrainingByIds<TrainingUserViewModel>(trainingId, userId);

            var expectedResult = this.dummyUserTrainings
                 .SingleOrDefault(ut => ut.UserId == userId && ut.TrainingId == trainingId)
                 .Training;

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.TrainingName.Equals(expectedResult.Name));
                Assert.That(actualResult.TrainingId.Equals(expectedResult.Id));
            });
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task GetCurrentUserTrainingByIds_WithDummyData_ShouldReturnCorrectResults()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.trainingService.GetCurrentUserTrainingsId();

            var expectedResult = this.dummyUserTrainings
                 .Where(x => x.UserId == currentUserId)
                 .Select(ut=> ut.TrainingId);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task GetTrainingById_ShouldReturnCorrectResult()
        {
            var trainingId = "1";

            var actualResult = await this.trainingService.GetTrainingById(trainingId);

            var expectedResult = this.dummyTrainings
                 .SingleOrDefault(t => t.Id == trainingId);

            Assert.That(actualResult.Equals(expectedResult));
        }
    }
}
