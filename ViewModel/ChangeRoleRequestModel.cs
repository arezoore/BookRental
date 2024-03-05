using System.ComponentModel.DataAnnotations;

namespace LoanBook.ViewModel
{
    public class ChangeRoleRequestModel
    {
        [Required]
        public string userId { get; set; }
    }
}