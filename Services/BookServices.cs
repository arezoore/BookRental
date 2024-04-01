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
            if(_context.UserBooks.Count()>0)
            {
                var rentedBooks = _context.UserBooks!.Where(ub=>ub.ReturnDate == null).ToList()
            .GroupBy(u=>u.BookId)
            .Select(x=> new{bookid=x.Key, rentedCount = x.Count()}).ToList();
            var bookList = await _context.Books!.ToListAsync();
            foreach(var book in bookList){
                var rentedBookObj = rentedBooks.Where(x=> x.bookid == book.BookId).FirstOrDefault();
                if(rentedBookObj != null){
                    book.Quantity = book.Quantity - rentedBookObj.rentedCount;
                }
            }
            return bookList;
            }
            return new List<Book>();
        } 
        public async Task<Book?> GetBookDetailAsync(int id)
        {
            return await _context.Books!.FirstOrDefaultAsync(x => x.BookId == id);
        }     
        public async Task<List<UserBook>> GetRentedBookListAsync()
        {
            return await _context.UserBooks!.Where(ub=>ub.ReturnDate == null).Include(x=>x.XtendUser).Include(y=>y.Book).ToListAsync();
        } 
    }
}