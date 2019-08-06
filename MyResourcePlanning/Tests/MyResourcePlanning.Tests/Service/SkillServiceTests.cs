using MyResourcePlanning.Models;
using MyResourcePlanning.Services.Data.Skill;
using MyResourcePlanning.Services.Data.SkillCategory;
using MyResourcePlanning.Services.Data.User;
using MyResourcePlanning.Tests.Common;
using MyResourcePlanning.Web.ViewModels.Skill;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResourcePlanning.Tests.Service
{
    [TestFixture]
    public class SkillServiceTests
    {
        private ISkillService skillService;
        private ISkillCategoryService skillCategoryService;
        private IUserService userService;
        private List<Skill> dummySkills;
        private List<SkillCategory> dummySkillCategories;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.skillCategoryService = new SkillCategoryService(context);
            this.userService = new UserService(context, null);
            this.skillService = new SkillService(context, skillCategoryService, userService);

            this.dummySkillCategories = DummyData.GetDummySkillCategories();
            this.dummySkillCategories = DummyData.GetDummySkillCategories();

            context.AddRange(dummySkillCategories);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "UserService")]
        public async Task GetAllSkillsByCategories_WithDummyData_ShouldReturnCorrectResults()
        {
            var actualResults = await this.skillService.GetAllSkillsByCategories<SkillCategoryViewModel>();

            var expectedResults = this.dummySkillCategories
                .Where(s => s.IsDeleted == false)
                .OrderBy(s=> s.Name)
                .ToList();

            CollectionAssert.AreEqual(actualResults.Select(x => x.Id), expectedResults.Select(x => x.Id));
        }
    }
}
