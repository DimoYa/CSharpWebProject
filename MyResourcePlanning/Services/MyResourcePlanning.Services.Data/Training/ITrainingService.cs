using MyResourcePlanning.Web.BindingModels.Training;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyResourcePlanning.Services.Data.Training
{
    public interface ITrainingService
    {
        Task<IEnumerable<TViewModel>> GetAllTrainings<TViewModel>();

        Task<bool> Create(TrainingCreateBindingModel inputModel);
    }
}
