namespace MyResourcePlanning.Tests.Service
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Tests.Common;
    using MyResourcePlanning.Web.ViewModels.User;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class UserServiceTests
    {
        private IUserService userService;
        private List<UserRole> dummyRoles;
        private List<User> dummyUsers;

        [SetUp]
        [Property("service", "UserService")]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.userService = new UserService(context, null);

            this.dummyRoles = DummyData.GetDummyUserRoles();
            this.dummyUsers = DummyData.GetDummyUsers();

            context.AddRange(dummyRoles);
            context.AddRange(dummyUsers);
            context.SaveChanges();
            
            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "UserService")]
        public async Task GetAllActiveResources_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.userService.GetAllActiveResources<UsersViewModel>();

            var expectedResults = this.dummyUsers
                .Where(u => u.Roles.Any(r => r.RoleId == "111"))
                .Where(u=> u.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(x=> x.Id), expectedResults.Select(x=> x.Id));
        }

        [Test]
        [Property("service", "UserService")]
        public async Task GetAllActiveApprovers_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.userService.GetAllActiveApprovers<UsersViewModel>();

            var expectedResults = this.dummyUsers
                .Where(u => u.Roles.Any(r => r.RoleId == "222"))
                .Where(u => u.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(x => x.Id), expectedResults.Select(x => x.Id));
        }

        [Test]
        [TestCase("Resource", "111")]
        [TestCase("Approver", "222")]
        [Property("service", "UserService")]
        public async Task GetRoleIdByName_WithDummyData_ShouldReturnCorrectResults(string roleName, string roleId)
        {
            var actualResults = await this.userService.GetRoleIdByName(roleName);

            var expectedResults = roleId;

            Assert.That(actualResults.Equals(expectedResults));
        }

        [Test]
        [TestCase("111", "Resource")]
        [TestCase("222", "Approver")]
        [Property("service", "UserService")]
        public async Task GetRoleNameById_WithDummyData_ShouldReturnCorrectResults(string roleId, string roleName)
        {
            var actualResults = await this.userService.GetRoleNameById(roleId);

            var expectedResults = roleName;

            Assert.That(actualResults.Equals(expectedResults));
        }

        [Test]
        [Property("service", "UserService")]
        public async Task GetUserByName_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.userService.GetUserByName("FirstName", "LastName");

            var expectedResults = this.dummyUsers
                .SingleOrDefault(u=> u.FirstName == "FirstName" && u.LastName == "LastName");

            Assert.That(actualResults.Equals(expectedResults));
        }

        [Test]
        [TestCase("123")]
        [TestCase("124")]
        [Property("service", "UserService")]
        public async Task GetUserById_WithDummyData_ShouldReturnCorrectResults(string userId)
        {
            var actualResults = await this.userService.GetUserById(userId);

            var expectedResults = this.dummyUsers
                .SingleOrDefault(u => u.Id == userId);

            Assert.That(actualResults.Equals(expectedResults));
        }
    }
}

