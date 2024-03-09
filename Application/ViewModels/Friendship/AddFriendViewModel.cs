using System.ComponentModel.DataAnnotations;

namespace SocialMedia_App.Core.Application.ViewModels.Friendship
{
    public class AddFriendViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        public string UserName { get; set; }
    }
}
