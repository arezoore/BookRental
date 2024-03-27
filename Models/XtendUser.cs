using Microsoft.AspNetCore.Identity;

namespace LoanBook.Models
{
    public class XtendUser:IdentityUser
    {
        public XtendUser() : base(){ }
        public string? FirstName { get; set; }
        public string? FamilyName { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}