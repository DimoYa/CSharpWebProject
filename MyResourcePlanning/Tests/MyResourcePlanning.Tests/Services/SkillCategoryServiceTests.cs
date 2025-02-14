﻿namespace MyResourcePlanning.Tests.Service
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Tests.Common;
    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Web.ViewModels.Skill;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SkillCategoryServiceTests
    {
        private ISkillCategoryService skillCategoryService;
        private List<SkillCategory> dummySkillCategories;

        [SetUp]
        public void Setup()
        {
            var context = MyResourcePlanningDbContextInMemoryFactory.InitializeContext();
            this.skillCategoryService = new SkillCategoryService(context);

            this.dummySkillCategories = DummyData.GetDummySkillCategories();

            context.AddRange(dummySkillCategories);
            context.SaveChanges();

            MapperInitializer.InitializeMapper();
        }

        [Test]
        [Property("service", "SkillCategoryService")]
        public async Task CreateCategory_ShouldReturnCorrectResults()
        {
            var mockedModel = new SkillCategoryCreateBindingModel()
            {
                Name = "TestCategoryName"
            };

            var actualResults = await this.skillCategoryService.CreateCategory(mockedModel);

            Assert.IsTrue(actualResults);
        }

        [Test]
        [Property("service", "SkillCategoryService")]
        public async Task UpdateCategory_ShouldReturnCorrectResults()
        {
            var categoryId = "10";
            var newCategoryName = "TestCategoryNameUpdated";
            var mockedModel = new SkillCategoryEditBindingModel()
            {
                Name = newCategoryName
            };

            var categoryForUpdate = this.dummySkillCategories
                .SingleOrDefault(s => s.Id == categoryId);

            await this.skillCategoryService.EditCategory(mockedModel, categoryId);
            var actualResult = this.dummySkillCategories
                .SingleOrDefault(s => s.Id == categoryId)
                .Name;

            Assert.That(actualResult.Equals(newCategoryName));
        }

        [Test]
        [Property("service", "SkillCategoryService")]
        public async Task DeleteCategory_ShouldSetIsDeletedFlag()
        {
            var categoryId = "10";

            await this.skillCategoryService.DeleteCategory(categoryId);

            var actualResult = this.dummySkillCategories
                .SingleOrDefault(s => s.Id == categoryId)
                .IsDeleted;

            Assert.IsTrue(actualResult);
        }

        [Test]
        [Property("service", "SkillCategoryService")]
        public async Task GetCategoryByName_ShouldReturnCorrectResults()
        {
            var categoryName = "Category2";

            var actualResult = await this.skillCategoryService
                .GetCategoryByName(categoryName);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult != null);
                Assert.That(actualResult.Name.Equals(categoryName));
            });
        }

        [Test]
        [Property("service", "SkillCategoryService")]
        public async Task GetCategoryById_ShouldReturnCorrectResults()
        {
            var categoryId = "11";

            var actualResult = await this.skillCategoryService
                .GetCategoryById(categoryId);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult != null);
                Assert.That(actualResult.Id.Equals(categoryId));
            });
        }

        [Test]
        [Property("service", "SkillCategoryService")]
        public async Task GetAllActiveSkillCategories_ShouldReturnCorrectResults()
        {
            var actualResults = await this.skillCategoryService
                .GetAllActiveSkillCategories<SkillCategoryViewModel>();

            var expectedResults = this.dummySkillCategories
                .Where(u => u.IsDeleted == false)
                .ToList();

            CollectionAssert.AreEqual(
                expectedResults.Select(s => s.Id),
                actualResults.Select(s => s.Id));
        }
    }
}
