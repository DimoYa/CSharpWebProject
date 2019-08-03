namespace MyResourcePlanning.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class UserDetails
    {
        public string Id { get; set; }

        public string FreeHours { get; set; }

        public List<UserSkill> Skills { get; set; }

        public List<UserTraining> Trainings { get; set; }

    }
}
