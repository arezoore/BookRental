using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoanBook.Models;
using LoanBook.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace LoanBook.Controllers;

public class HomeController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly UserManager<XtendUser> _usermanager;

    public HomeController(ApplicationDbContext context, UserManager<XtendUser> usermanager)
    {
        _context = context;
        _usermanager = usermanager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _usermanager.GetUserAsync(HttpContext.User);
        if(User.Identity.IsAuthenticated)
        {
            List<UserBook> MyBookList = _context.UserBooks.Where(x=>x.XtendUserId == user.Id)
            .Include(x=>x.Book)
            .ToList();
            return View(MyBookList);
        }
        return View(new List<UserBook>());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
