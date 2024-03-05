using LoanBook.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanBook.Data
{
public static class SampleData {
    // this is an extension method to the ModelBuilder class
    public static void Seed(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<Book>().HasData(
            GetBooks()
        );
    }
    public static List<Book> GetBooks() {
        List<Book> books = new List<Book>() {
            new Book(){
                BookId = 4,
                AuthorName = "Andrew Chevallier",
                Title = "Encyclopedia of Herbal Medicine: 550 Herbs and Remedies for Common Ailments",
                PublishedYear = 2016,
                Quantity = 1
            },
                new Book(){
                BookId = 6,
                AuthorName = "Michael T. Murray M.D. and Joseph Pizzorno",
                Title = "The Encyclopedia of Natural Medicine Third Edition",
                PublishedYear = 2012,
                Quantity = 3
            },
                new Book(){
                BookId = 7,
                AuthorName = "Thomas Easley and Steven Horne",
                Title = "The Modern Herbal Dispensatory: A Medicine- Making Guide",
                PublishedYear = 2016,
                Quantity = 1
            },
                new Book(){
                BookId = 8,
                AuthorName = "Cat Ellis",
                Title = "Prepper's Natural Medicine: Life-Saving Herbs, Essential Oils and Natural Remedies for When There is No Doctor",
                PublishedYear = 2015,
                Quantity = 2
            },
                new Book(){
                BookId = 9,
                AuthorName = "Rosemary Gladstar",
                Title = "Rosemary Gladstar's Medicinal Herbs: A Beginner's Guide: 33 Healing Herbs to Know, Grow, and Use",
                PublishedYear = 2012,
                Quantity = 1
            }
        };

        return books;
    }

    
}
}
