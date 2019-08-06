namespace MyResourcePlanning.Tests.Service
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.Skill;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Tests.Common;
    using MyResourcePlanning.Web.ViewModels.Skill;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using System.Threading.Tasks;
    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Models.Enums;

    [TestFixture]
    public class SkillServiceTests
    {
        private ISkillService skillService;
        private ISkillCategoryService skillCategoryService;
        private Mock<IUserService> mockedUserService;
        private List<Skill> dummySkills;
        private List<SkillCategory> dummySkillCategories;
        private List<UserSkill> dummyUserSkills;
        private List<User> dummyUsers;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.skillCategoryService = new SkillCategoryService(context);
            this.mockedUserService = new Mock<IUserService>();

            this.skillService = new SkillService(context, skillCategoryService, mockedUserService.Object);

            this.dummySkillCategories = DummyData.GetDummySkillCategories();
            this.dummySkills = DummyData.GetDummySkills();
            this.dummyUsers = DummyData.GetDummyUsers();
            this.dummyUserSkills = DummyData.GetDummyUserSkills();

            context.AddRange(dummySkillCategories);
            context.AddRange(dummySkills);
            context.AddRange(dummyUsers);
            context.AddRange(dummyUserSkills);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetAllSkillsByCategories_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.skillService.GetAllSkillsByCategories<SkillCategoryViewModel>();

            var expectedResults = this.dummySkillCategories
                .Where(s => s.IsDeleted == false)
                .OrderBy(s => s.Name)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(x => x.Id), expectedResults.Select(x => x.Id));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetUserSkillsByCategories_WithDummyData_ShouldReturnOnlyActiveSkills()
        {
            var currentUserId = "123";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
              .Returns(Task.FromResult(currentUserId));

            var actualResults = await this.skillService.GetUserSkillsByCategories<UserSkillsByCategoryViewModel>();

            var expectedResults = this.dummyUserSkills
                .Where(us => us.UserId == currentUserId)
                .Where(s => s.Skill.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(actualResults
                .Select(x => x.SkillId),

                expectedResults
                .Select(x => x.SkillId));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetUserSkillsByCategories_WithoutSkill_ShouldNotreturnException()
        {
            var currentUserId = "127";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResults = await this.skillService.GetUserSkillsByCategories<UserSkillsByCategoryViewModel>();

            Assert.That(actualResults.Count().Equals(0));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task CreateSkill_WithDummyData_ShouldReturnCorrectResults()
        {
            var mockedModel = new SkillCreateBindingModel()
            {
                Name = "NewSkill",
                SkillCategory = "Category2"
            };

            var actualResults = await this.skillService.CreateSkill(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task UpdateSkill_WithDummyData_ShouldReturnCorrectResults()
        {
            var skillId = "1";
            var newSkillName = "Category2";

            var mockedModel = new SkillEditBindingModel()
            {
                Name = newSkillName
            };

            var skillForUpdate = this.dummySkills.SingleOrDefault(s => s.Id == skillId);

            await this.skillService.EditSkill(mockedModel, skillId);

            var actualResult = this.dummySkills
                .SingleOrDefault(s => s.Id == skillId)
                .Name;

            Assert.That(actualResult.Equals(newSkillName));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task DeleteSkill_ShouldSetIsDeletedFlag()
        {
            var skillId = "3";

            await this.skillService.DeleteSkill(skillId);

            var actualResult = this.dummySkills
                .SingleOrDefault(s => s.Id == skillId)
                .IsDeleted;

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task AddSkillToMyProfile_ShouldSkillToUserProfile()
        {
            var currentUserId = "125";
            var skillId = "2";
            var skillName = "Skill2";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var mockedModel = new SkillAddBindingModel()
            {
                Name = "skillName",
                SkillLevel = SkillLevel.Beginning
            };

            await this.skillService.AddSkillToMyProfile(skillId, mockedModel);

            var actualResults = this.dummyUsers
                .SingleOrDefault(u => u.Id == currentUserId);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(actualResults.Skills.Any(s => s.SkillId == skillId));

                Assert.That(actualResults.Skills
                    .Where(s => s.SkillId == skillId)
                    .SingleOrDefault()
                    .Skill.Name.Equals(skillName));

                Assert.That(actualResults.Skills
                     .Where(s => s.SkillId == skillId)
                     .SingleOrDefault()
                     .Level.Equals(SkillLevel.Beginning));
            });
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task RemoveSkillFromMyProfile_ShouldRemoveSkillFromUserProfile()
        {
            var currentUserId = "125";
            var skillIdToRemove = "3";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            await this.skillService.RemoveSkillFromProfile(skillIdToRemove);

            var actualResults = this.dummyUsers
                .SingleOrDefault(u => u.Id == currentUserId);

            Assert.IsEmpty(actualResults.Skills.Where(s => s.SkillId == skillIdToRemove));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetSkillById_ShouldReturnCorrectSkill()
        {
            var skillId = "3";

            await this.skillService.GetSkillById(skillId);

            var actualResult = await this.skillService.GetSkillById(skillId);
            var expectedResult = this.dummySkills.FirstOrDefault(s => s.Id == skillId);

            Assert.That(actualResult.Equals(expectedResult));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetCurrentuserSkillById_ShouldReturnCorrectUserSkill()
        {
            var currentUserId = "124";
            var skillId = "2";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.skillService.GetCurrentuserSkillById(skillId);
            var expectedResult = this.dummyUserSkills
                .FirstOrDefault(s => s.SkillId == skillId && s.UserId == currentUserId);

            Assert.That(actualResult.Equals(expectedResult));
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task GetUserSkillsId_ShouldReturnAllActivetUserSkillsId()
        {
            var currentUserId = "124";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var actualResult = await this.skillService.GetCurrentUserSkillsId();

            var expectedResult = this.dummyUserSkills
                .Where(us => us.UserId == currentUserId)
                .Select(us => us.SkillId);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        [Property("service", "SkillService")]
        public async Task EditSkillLevel_ShouldReturnNewSkillLevel()
        {
            var currentUserId = "123";
            var skillId = "2";

            this.mockedUserService.Setup(x => x.GetCurrentUserId())
             .Returns(Task.FromResult(currentUserId));

            var mockedModel = new SkillEditLevelBindingModel()
            {
                SkillLevel = SkillLevel.Medium
            };

            await this.skillService.EditSkillLevel(mockedModel, skillId);

            var actualResult = this.dummyUserSkills
                .SingleOrDefault(us => us.UserId == currentUserId && us.SkillId == skillId)
                .Level;

            var expectedResults = SkillLevel.Medium;

            Assert.That(actualResult.Equals(expectedResults));
        }
    }
}
