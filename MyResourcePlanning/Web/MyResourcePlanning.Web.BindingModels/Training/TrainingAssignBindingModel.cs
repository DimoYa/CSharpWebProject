using MyResourcePlanning.Web.ViewModels.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResourcePlanning.Web.BindingModels.Training
{
    public class TrainingAssignBindingModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Resource")]
        public string Resource { get; set; }

        public IEnumerable<UsersViewModel> Resources { get; set; }
    }
}
