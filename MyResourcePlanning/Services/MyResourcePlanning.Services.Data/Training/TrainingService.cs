namespace MyResourcePlanning.Services.Data.Training
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Training;

    public class TrainingService : ITrainingService
    {
        private readonly MyResourcePlanningDbContext context;

        public TrainingService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(TrainingCreateBindingModel model)
        {
            Training training = new Training
            {
                Name = model.Name,
                Type = model.Type,
                DueDate = model.DueDate,
                Status = model.Status,
            };

            this.context.Trainings.Add(training);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<TViewModel>> GetAllTrainings<TViewModel>()
        {
            var trainings = this.context.Trainings
                .Where(t => t.IsDeleted == false)
                .OrderBy(t => t.Name)
                .To<TViewModel>()
                .ToList();

            return trainings;
        }
    }
}
