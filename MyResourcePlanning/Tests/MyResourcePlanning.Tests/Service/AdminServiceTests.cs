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

namespace MyResourcePlanning.Tests.Service
{
    [TestFixture]
    public class AdminServiceTests
    {
        private IAdminService adminService;
        private IUserService userService;
        private Mock<UserManager<User>> mockedUserManager;
        private List<UserRole> dummyRoles;
        private List<User> dummyUsers;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.userService = new UserService(context, null);

            this.mockedUserManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object);




            this.adminService = new AdminService(context, userService, mockedUserManager.Object);

            this.dummyRoles = DummyData.GetDummyUserRoles();
            this.dummyUsers = DummyData.GetDummyUsers();

            context.AddRange(dummyRoles);
            context.AddRange(dummyUsers);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task GetAllActiveUsers_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.adminService.GetAllActiveUsers<AdminAllUsersViewModel>();

            var expectedResults = this.dummyUsers
                .Where(u => u.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(x => x.Id), expectedResults.Select(x => x.Id));
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task GetUserRolesById_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.adminService.GetUserRolesById("123");

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
        public async Task GetUserApproverById_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.adminService.GetUserApproverById("123");
            var expectedResults = "Approver Approver";

            Assert.That(actualResults.CurrentApprover.Equals(expectedResults));
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task ManageUserApprover_WithDummyData_ShouldReturnCorrectResults()
        {
            var mockedModel = new AdminManageApproverBindingModel() { FullName = "Test Test" };

            await this.adminService.ManageUserApprover("123", mockedModel);

            var actualResult = dummyUsers.SingleOrDefault(u => u.Id == "123");

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Approver.FirstName.Equals("Test"));
                Assert.That(actualResult.Approver.LastName.Equals("Test"));
            });
        }

        [Test]
        [Property("service", "AdminService")]
        public async Task Lock_ShouldLocksTheUser()
        {
            var userid = "123";
            var user = this.dummyUsers.SingleOrDefault(u=>u.id == userid);

            this.mockedUserManager.Setup(x => x.SetLockoutEndDateAsync(user, Datetime.now))
            .Returns(Task.FromResult(currentUserId));

            await this.adminService.Lock(userid);

            var actualResult = dummyUsers.SingleOrDefault(u => u.Id == "123");

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Approver.FirstName.Equals("Test"));
                Assert.That(actualResult.Approver.LastName.Equals("Test"));
            });
        }
    }
}
