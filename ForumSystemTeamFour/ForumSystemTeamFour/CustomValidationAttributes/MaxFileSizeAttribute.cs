using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.CustomValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int MaxFileSize = 1024000;        

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > MaxFileSize)
                {
                    return new ValidationResult("Your profile picture must be smaller than 1MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
