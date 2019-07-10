using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyResourcePlanning.Data.Models;
using MyResourcePlanning.Web.Infrastructure.Validators;
using MyResourcePlanning.Web.ViewModels.Project;
using MyResourcePlanning.Web.ViewModels.User;

namespace MyResourcePlanning.Web.ViewModels.Request
{
    public class RequestCreateBindingModel
    {

        [Required]
        [DateGreaterOrEqualThatPresent]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        [DateGreaterOrEqualThatPresent]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Projects")]
        public string Project { get; set; }

        [Required]
        [Display(Name = "Resources")]
        public string Resource { get; set; }
    }
}
