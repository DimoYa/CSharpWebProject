namespace MyResourcePlanning.Web.BindingModels.Training
{
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Models;
    using System.ComponentModel.DataAnnotations;
    public class TrainingRequestBindingModel : IMapFrom<Training>
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
