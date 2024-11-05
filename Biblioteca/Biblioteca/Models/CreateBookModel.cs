using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class CreateBookModel
    {
        [Key]
        [Column(TypeName = "int")]
        public int BookId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is required.")]
        public string NomeLivro { get; set; }

        public int Quantitie { get; set; }
    }
}
