using Moq;
using MyResourcePlanning.Models;
using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Services.Data.Training;
using MyResourcePlanning.Services.Data.User;
using MyResourcePlanning.Tests.Common;
using MyResourcePlanning.Web.BindingModels.Training;
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

            await this.trainingService.Request(trainingId, mockedModel);

            var actualResult = this.dummyUserTrainings
               .SingleOrDefault(ut => ut.UserId == currentUserId && ut.TrainingId == trainingId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(actualResult);
                Assert.That(actualResult.Status.Equals(UserTrainingStatus.Requested));
            });
        }

        [Test]
        [Property("service", "TrainingService")]
        public async Task AssignToUser_ShouldAssignUserToTraining()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            var trainingId = "2";
            var userId = "123";

            this.mockedUserService.Setup(x => x.GetUserByName("FirstName", "LastName"))
            .Returns(Task.FromResult(context.Users.SingleOrDefault(u=> u.Id == userId)));

            var mockedModel = new TrainingAssignBindingModel()
            {
                Name = "Training2",
                Resource = "FirstName LastName",
            };

            await this.trainingService.AssignToUser(trainingId, mockedModel);

            var actualResult = this.dummyUserTrainings
               .SingleOrDefault(ut => ut.UserId == userId && ut.TrainingId == trainingId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(actualResult);
                Assert.That(actualResult.Status.Equals(UserTrainingStatus.Assigned));
            });
        }
    }
}
