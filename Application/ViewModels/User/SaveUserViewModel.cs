using System;
using System.ComponentModel.DataAnnotations;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.ViewModels.User
{
	public class SaveUserViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [Compare(nameof(Password), ErrorMessage = "No coinciden las contraseñas ingresadas")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe asignar un rol al usuario")]
        public UserRole Role { get; set; }
    }
}

