using Microsoft.AspNetCore.Identity;

namespace LoanBook.Models
{
    public class XtendUser:IdentityUser
    {
        public XtendUser() : base(){ }
        public string? FirstName { get; set; }
        public string? FamilyName { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}