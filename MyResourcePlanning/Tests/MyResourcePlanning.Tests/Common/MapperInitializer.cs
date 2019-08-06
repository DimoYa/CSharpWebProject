namespace MyResourcePlanning.Tests.Common
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.ViewModels.Admin;
    using MyResourcePlanning.Web.ViewModels.Skill;
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
        }
    }
}
