using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia_App.Core.Application.ViewModels.User
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+?1)?(809|829|849)\d{3}\d{4}$", ErrorMessage = "Debe ingresar un número de teléfono válido de República Dominicana")]
        public string Phone { get; set; }

        public string? ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }


        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas ingresadas no coinciden")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
