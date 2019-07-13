namespace MyResourcePlanning.Data.Models
{
    using MyResourcePlanning.Data.Models.BaseModels;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
