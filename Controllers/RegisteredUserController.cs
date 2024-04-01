using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoanBook.Models;
using LoanBook.Data;
using Microsoft.AspNetCore.Identity;
using LoanBook.ViewModel;


namespace LoanBook.Controllers
{
    public class RegisteredUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<XtendUser> _usermanager;


        public RegisteredUserController(ApplicationDbContext context, UserManager<XtendUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }
        public async Task<IActionResult> Index()
        {
                var usersWithoutRole = await _context.Users
                .Where(u => !_context.UserRoles.Any(ur => ur.UserId == u.Id))
                .ToListAsync();

            return View(usersWithoutRole);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(ChangeRoleRequestModel model)
        {
            var user = await _usermanager.FindByIdAsync(model.userId);
            if (user == null)
            {
                return NotFound();
            }
            await _usermanager.AddToRoleAsync(user, "Member");

            return Ok();
        }
    }
}