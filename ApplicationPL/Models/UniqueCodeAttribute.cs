using System.ComponentModel.DataAnnotations;


namespace ApplicationPL.Models
{
    public class UniqueCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not null) 
            {
                string convertedName = (string)value;

                if (convertedName.Contains("Route")) 
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return null; ;
        }
    }
}
