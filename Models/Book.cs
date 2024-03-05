using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LoanBook.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public int? PublishedYear { get; set; }
        public int? Quantity { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}