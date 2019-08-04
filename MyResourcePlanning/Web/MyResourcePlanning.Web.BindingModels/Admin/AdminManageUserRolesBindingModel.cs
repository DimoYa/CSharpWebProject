using System.Collections.Generic;

namespace MyResourcePlanning.Web.BindingModels.Admin
{
    public class AdminManageUserRolesBindingModel
    {
        public string FullName { get; set; }
        public bool Resource { get; set; }
        public bool Admin { get; set; }
        public bool Approver { get; set; }
        public bool Planner { get; set; }
    }
}
