namespace MyResourcePlanning.Web.Infrastructure.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateGreaterThan : ValidationAttribute
    {
        private string startDatePropertyName;

        public DateGreaterThan(string startDatePropertyName)
        {
            this.startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(this.startDatePropertyName);
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
            if ((DateTime)value >= (DateTime)propertyValue)
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
