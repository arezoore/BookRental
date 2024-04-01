using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LoanBook.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name ="Author Name")]
        public string? AuthorName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [Display(Name ="Book Title")]
        public string? Title { get; set; }

        [Required]
        [Range(1800, 2040, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [Display(Name ="Published Year")]
        public int? PublishedYear { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [Display(Name ="Quanitity")]
        public int? Quantity { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}