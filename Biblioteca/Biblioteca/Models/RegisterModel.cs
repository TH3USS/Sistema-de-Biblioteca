using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class RegisterModel
    {
        [Key]
        public int RigisterId { get; set; }       

        [Range(1, int.MaxValue, ErrorMessage = "Please select a book.")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public CreateBookModel? Book { get; set; }

        public bool Returned { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Emprestimo { get; set; } = DateTime.Now;

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Return date is required.")]
        public DateTime Devolucao { get; set; }


    }
}
