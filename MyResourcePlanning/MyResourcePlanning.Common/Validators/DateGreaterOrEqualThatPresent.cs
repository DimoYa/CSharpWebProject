namespace MyResourcePlanning.Common.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateGreaterOrEqualThatPresent : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime d = Convert.ToDateTime(value);

            if (d.Date >= DateTime.Today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date can not be in the past");
            }
        }
    }
}
