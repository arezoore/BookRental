using LoanBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoanBook.Data;

public class ApplicationDbContext : IdentityDbContext<XtendUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){ }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Book>().ToTable("Book");
        builder.Entity<UserBook>(x=>{
            x.ToTable("UserBook");
            x.HasOne(b=>b.Book).WithMany(u=>u.UserBooks).HasForeignKey(b=>b.BookId);
        });

        

        builder.Entity<Book>().HasData(SampleData.GetBooks());
    }

    public DbSet<Book>? Books { get; set;}
    public DbSet<UserBook>? UserBooks {get; set;}
}
