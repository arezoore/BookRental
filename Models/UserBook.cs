using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanBook.Models
{
    public class UserBook
    {
        [Key]
        public int UserBookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public string? XtendUserId { get; set; }

        public Book? Book { get; set; }
        public XtendUser? XtendUser {get; set; }

    }
}