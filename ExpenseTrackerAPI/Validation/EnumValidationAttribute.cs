using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Validation;

    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValidationAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !Enum.IsDefined(_enumType, value.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? "Invalid value for the enum");
            }
            return ValidationResult.Success;
        }
}