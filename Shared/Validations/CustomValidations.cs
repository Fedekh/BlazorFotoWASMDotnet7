using BlazorFotoWASMDotnet7.Shared.DB;
using System.ComponentModel.DataAnnotations;

namespace BlazorFotoWASMDotnet7.Server.Validations
{
    public class CustomValidations:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = (FotoContext)validationContext.GetService(typeof(FotoContext));

            var name = (string)value;
            var existingFoto = dbContext.Foto.FirstOrDefault(f => f.Name == name);

            return existingFoto != null
                ? new ValidationResult("Il nome deve essere unico.")
                : ValidationResult.Success;
        }
    }
}
