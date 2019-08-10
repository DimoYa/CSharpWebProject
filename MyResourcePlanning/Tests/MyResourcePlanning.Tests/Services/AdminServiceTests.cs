namespace MyResourcePlanning.Tests.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.Admin;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Tests.Common;
    using MyResourcePlanning.Web.BindingModels.Admin;
    using MyResourcePlanning.Web.ViewModels.Admin;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class AdminServiceTests
    {
        private IAdminService adminService;
        private IUserService userService;
        private Mock<UserManager<User>> mockUserManager;
        private List<UserRole> dummyRoles;
        private List<User> dummyUsers;

        [SetUp]
        public void Setup()
        {
           var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.userService = new UserService(context, null);

            this.mockUserManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object);

            this.adminService = new AdminService(context, userService, mockUserManager.Object);

            this.dummyRoles = DummyData.GetDummyUserRoles();
            this.dummyUsers = DummyData.GetDummyUsers();

            context.AddRange(dummyRoles);
            context.AddRange(dummyUsers);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task GetAllActiveUsers_ShouldReturnCorrectResults()
        {
            var actualResults = await this.adminService.GetAllActiveUsers<AdminAllUsersViewModel>();

            var expectedResults = this.dummyUsers
                .Where(u => u.IsDeleted == false)
                .OrderBy(u=> u.FirstName)
                .ThenBy(u=> u.LastName)
                .ToList();

            CollectionAssert.AreEqual(
                expectedResults.Select(x => x.Id),
                actualResults.Select(x => x.Id));
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task GetUserRolesById_ShouldReturnCorrectResults()
        {
            var userId = "123";

            var actualResults = await this.adminService.GetUserRolesById(userId);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(actualResults.Resource);
                Assert.IsTrue(actualResults.Approver);
                Assert.IsFalse(actualResults.Admin);
                Assert.IsFalse(actualResults.Planner);
            });
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task GetUserApproverById_ShouldReturnCorrectResults()
        {
            var approverId = "123";

            var actualResults = await this.adminService
                .GetUserApproverById(approverId);
            var expectedResults = "Approver Approver";

            Assert.That(actualResults.CurrentApprover.Equals(expectedResults));
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task ManageUserApprover_ShouldReturnCorrectResults()
        {
            var userId = "123";

            var mockedModel = new AdminManageApproverBindingModel()
            {
                FullName = "Test Test"
            };

            await this.adminService.ManageUserApprover(userId, mockedModel);

            var actualResult = dummyUsers.SingleOrDefault(u => u.Id == userId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Approver.FirstName.Equals("Test"));
                Assert.That(actualResult.Approver.LastName.Equals("Test"));
            });
        }
    }
}
