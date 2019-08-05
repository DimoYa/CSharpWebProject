namespace MyResourcePlanning.Web.ViewModels.Admin
{
    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class AdminUserApproversViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
