using LoanBook.Data;
using LoanBook.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanBook.Services
{
    public class BookServices
    {
        private ApplicationDbContext _context;
        public BookServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBookListAsync()
        {
            return await _context.Books.ToListAsync();
        } 
        public async Task<Book> GetBookDetailAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);
        }     


    }
}