using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Zeemlin.Service.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UzbekistanPassportAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Passport seriya majburiy.");
            }

            string passportNumber = value.ToString()!;

            // Regular expression for Uzbekistan passport number format (2 letters, 7 digits)
            Regex regex = new Regex("^[A-Z]{2}[0-9]{7}$");

            if (!regex.IsMatch(passportNumber))
            {
                return new ValidationResult("Invalid Uzbekistan passport number format. It should be XX-1234567 (XX - two letters, 1234567 - seven digits).");
            }

            return ValidationResult.Success;
        }
    }
}
