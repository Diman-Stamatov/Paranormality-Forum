using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ForumSystemTeamFour.CustomValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] Extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            Extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!Extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"Please upload either a .jpg or a .png file!");
                }
            }

            return ValidationResult.Success;
        }

    }
}
