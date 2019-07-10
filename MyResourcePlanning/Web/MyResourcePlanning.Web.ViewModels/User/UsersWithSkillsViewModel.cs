using System.Collections.Generic;

namespace MyResourcePlanning.Web.ViewModels.User
{
    public class UsersWithSkillsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Skills { get; set; }
    }
}
