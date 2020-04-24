namespace MyResourcePlanning.Tests.Service
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Tests.Common;
    using MyResourcePlanning.Web.BindingModels.Project;
    using MyResourcePlanning.Web.ViewModels.Project;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class ProjectServiceTests
    {
        private IProjectService projectService;
        private List<Project> dummyProjects;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.projectService = new ProjectService(context);

            this.dummyProjects = DummyData.GetDummyProjects();

            context.AddRange(dummyProjects);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task CreateProject_ShouldReturnCorrectResults()
        {
            var mockedModel = new ProjectCreateBindingModel()
            {
                Name = "New",
                StartDate = DateTime.Now.AddDays(-5),
                EndDate = DateTime.Now.AddDays(15),
                RequestedHours = 100,
            };

            var actualResults = await this.projectService.Create(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task EditProject_ShouldReturnCorrectResults()
        {
            var projectId = "1";
            var newName = "NewProjectName";
            var newStartDate = DateTime.Now;
            var newEndDate = DateTime.Now.AddDays(18);
            var hours = 5;

            var mockedModel = new ProjectEditBindingModel()
            {
                Name = newName,
                StartDate = newStartDate,
                EndDate = newEndDate,
                RequestedHours = hours,
            };

            await this.projectService.Edit(mockedModel, projectId);

            var actualResult = this.dummyProjects
                .SingleOrDefault(r => r.Id == projectId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.StartDate.Equals(newStartDate));
                Assert.That(actualResult.EndDate.Equals(newEndDate));
                Assert.That(actualResult.RequestedHours.Equals(hours));
                Assert.That(actualResult.Name.Equals(newName));

            });
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task DeleteProject_ShouldSetIsDeletedFlag()
        {
            var projectId = "2";

            await this.projectService.Delete(projectId);

            var actualResult = this.dummyProjects
                .SingleOrDefault(r => r.Id == projectId)
                .IsDeleted;

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task GetProjectById_ShouldReturnCorrectProject()
        {
            var projectId = "4";

            var actualResult = await this.projectService.GetProjectById(projectId);

            var expectedResult = this.dummyProjects
                .Where(p => p.IsDeleted == false)
                .SingleOrDefault(r => r.Id == projectId);

            Assert.That(actualResult.Equals(expectedResult));
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task GetProjectByName_ShouldReturnCorrectProject()
        {
            var projectName = "Project1";

            var actualResult = await this.projectService.GetProjectByName(projectName);

            var expectedResult = this.dummyProjects
                .Where(p => p.IsDeleted == false)
                .SingleOrDefault(r => r.Name == projectName);

            Assert.That(actualResult.Equals(expectedResult));
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task Mapproject_ShouldMapCorrectProject()
        {
            var projectId = "2";

            var actualResult = await this.projectService
                .MapProject<ProjectAllViewModel>(projectId);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ProjectAllViewModel>(actualResult);
                Assert.That(actualResult.Id.Equals(projectId));
                Assert.That(actualResult.Name.Equals("Project2"));
            });
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task GetAllProjects_ShouldReturnCorrectResults()
        {

            var actualResults = await this.projectService
                .GetAllProjects<ProjectAllViewModel>();

            var expectedResults = this.dummyProjects
                .Where(p => p.IsDeleted == false)
                .Where(p => p.RequestedHours > 0)
                .Where(p => p.EndDate.Date >= DateTime.Now.Date)
                .OrderBy(p => p.Name);

            CollectionAssert.AreEqual(
                expectedResults.Select(p=> p.Id),
                actualResults.Select(p => p.Id));
        }

        [Test]
        [Property("service", "ProjectService")]
        public async Task GetAllProjectsForRequest_ShouldReturnCorrectResults()
        {

            var actualResults = await this.projectService
                .GetAllProjectsForRequest<ProjectAllViewModel>();

            var expectedResults = this.dummyProjects
                .Where(p => p.IsDeleted == false)
                .Where(p => p.RequestedHours > 0)
                .Where(p => p.EndDate.Date >= DateTime.Now.Date)
                .OrderBy(p=> p.Name);

            CollectionAssert.AreEqual(
                expectedResults.Select(p => p.Id),
                actualResults.Select(p => p.Id));
        }
    }
}
