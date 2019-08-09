namespace MyResourcePlanning.Tests.Common
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Web.ViewModels.Admin;
    using MyResourcePlanning.Web.ViewModels.Skill;
    using MyResourcePlanning.Web.ViewModels.Training;
    using MyResourcePlanning.Web.ViewModels.User;
    using System.Reflection;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(UsersViewModel).GetTypeInfo().Assembly,
                typeof(User).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AdminAllUsersViewModel).GetTypeInfo().Assembly,
                typeof(User).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(SkillCategoryViewModel).GetTypeInfo().Assembly,
                typeof(SkillCategory).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(UserSkillsByCategoryViewModel).GetTypeInfo().Assembly,
                typeof(UserSkill).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(SkillCreateBindingModel).GetTypeInfo().Assembly,
                typeof(Skill).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(TrainingAllViewModel).GetTypeInfo().Assembly,
                typeof(Training).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(TrainingUserViewModel).GetTypeInfo().Assembly,
                typeof(UserTraining).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(RequestCreateBindingModel).GetTypeInfo().Assembly,
                typeof(Request).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(RequestEditBindingModel).GetTypeInfo().Assembly,
                typeof(Request).GetTypeInfo().Assembly);

        }
    }
}
