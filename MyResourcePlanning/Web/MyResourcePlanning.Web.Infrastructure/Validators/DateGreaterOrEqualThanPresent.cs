namespace MyResourcePlanning.Web.Infrastructure.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateGreaterOrEqualThanPresent : ValidationAttribute
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
                return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}
