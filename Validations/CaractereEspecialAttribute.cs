using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RentAPI.Validations
{
    public class CaractereEspecialAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (Regex.IsMatch(value.ToString(), @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("A senha precisa conter ao menos 1 caractere especial.");
        }

    }
}
