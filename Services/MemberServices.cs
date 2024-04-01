
using LoanBook.Data;
using LoanBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoanBook.Services
{
    public class MemberServices
    {
        private ApplicationDbContext _context;
        private readonly UserManager<XtendUser> _usermanager;
        public MemberServices(ApplicationDbContext context, UserManager<XtendUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }
        public async Task<List<XtendUser>> GetMemberListAsync()
        {
            return await _context.Users
                .Where(u => !_context.UserRoles.Any(ur => ur.UserId == u.Id))
                .ToListAsync();
        } 
    }
}