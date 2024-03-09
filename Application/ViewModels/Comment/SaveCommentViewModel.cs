using System.ComponentModel.DataAnnotations;

namespace SocialMedia_App.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese su comentario")]
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
