using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.User
{
    public class ResetPassViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
    }

}
