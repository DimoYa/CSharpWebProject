using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Web.Infrastructure.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyResourcePlanning.Web.BindingModels.Training
{
    public class TrainingEditBindingModel
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(TrainingType))]
        [Display(Name = "Training type")]
        public TrainingType Type { get; set; }

        [Required]
        [EnumDataType(typeof(TrainingStatus))]
        [Display(Name = "Training status")]
        public TrainingStatus Status { get; set; }

        [Required]
        [DateGreaterOrEqualThanPresent(ErrorMessage = "Date cannot be in the past")]
        [Display(Name = "Due date")]
        [DataType(DataType.Text)]
        public DateTime DueDate { get; set; }
    }
}
