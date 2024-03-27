using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoanBook.Data;
using LoanBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using NuGet.Versioning;

namespace LoanBook.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<XtendUser> _usermanager;

        public BooksController(ApplicationDbContext context, UserManager<XtendUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var rentedBooks = _context.UserBooks.Where(ub=>ub.ReturnDate == null).ToList()
            .GroupBy(u=>u.BookId)
            .Select(x=> new{bookid=x.Key, rentedCount = x.Count()}).ToList();
            var bookList = _context.Books.ToList();
            foreach(var book in bookList){
                var rentedBookObj = rentedBooks.Where(x=> x.bookid == book.BookId).FirstOrDefault();
                if(rentedBookObj != null){
                    book.Quantity = book.Quantity - rentedBookObj.rentedCount;
                }
            }

            return View(bookList);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,AuthorName,Title,PublishedYear,Quantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,AuthorName,Title,PublishedYear,Quantity")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }


        public async Task<IActionResult> ConfirmLoan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);  

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Loan(int BookId)
        {
            
            if (ModelState.IsValid)
            {
                var user = await _usermanager.GetUserAsync(HttpContext.User);
                UserBook userbook = new UserBook();
                userbook.BookId = BookId;
                userbook.XtendUserId = user.Id;
                userbook.StartDate = DateTime.UtcNow; 
                userbook.ExpDate = DateTime.UtcNow.AddDays(30);
                _context.Add(userbook);
                await _context.SaveChangesAsync();
                return Redirect("/");
            }
            return NotFound();
        }

         public async Task<IActionResult> ConfirmReturn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userbook = _context.UserBooks
                .Where(m => m.UserBookId == id)
                .Include(x=>x.Book)
                .ToList();
            if (userbook == null)
            {
                return NotFound();
            }

            return View(userbook.First());  

        }
        [HttpPost]
        public async Task<IActionResult> Return(int UserBookId)
        {
            var user = await _usermanager.GetUserAsync(HttpContext.User);
            var userbook = _context.UserBooks.Find(UserBookId);
            if(userbook != null){
                userbook.ReturnDate = DateTime.UtcNow;
                _context.Update(userbook);
                await _context.SaveChangesAsync();
                return Redirect("/");
            }
            return NotFound();

        }
    }
}

