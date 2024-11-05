using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class LoginModel
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Password is required.")]
        public string PasswordConfirm { get; set; }
    }
}
