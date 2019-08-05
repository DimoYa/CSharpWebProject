namespace MyResourcePlanning.Web.BindingModels.Admin
{
    using MyResourcePlanning.Models;
    using System.ComponentModel.DataAnnotations;
    public class AdminManageApproverBindingModel
    {
        [Display(Name = "Current Approver")]
        [DataType(DataType.Text)]
        public string CurrentApprover { get; set; }

        [Required]
        [Display(Name = "Approver")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }
    }
}
