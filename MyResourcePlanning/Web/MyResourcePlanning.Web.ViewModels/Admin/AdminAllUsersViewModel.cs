namespace MyResourcePlanning.Web.ViewModels.Admin
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class AdminAllUsersViewModel : IMapFrom<User>, IMapFrom<IdentityUserRole<string>>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public List<IdentityUserRole<string>> Roles { get; set; }

        public bool IsLocked { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, AdminAllUsersViewModel>()
                .ForMember(
                    u => u.FullName,
                    opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(
                    u => u.IsLocked,
                    opt => opt.MapFrom(u => u.LockoutEnd == null ? false : true));
        }
    }
}
