
using BookShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookShoppingCart.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddBook(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteBook(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<Book?> GetBookById(int id) => await _db.Books.FindAsync(id);



        public async Task<(IEnumerable<Book> Books, int TotalCount)> GetBooks(string sTerm = "", int genreId = 0, int pageNumber = 1, int pageSize = 9)
        {
            sTerm = sTerm.ToLower();

            var query = _db.Books
                            .Include(b => b.Genre)
                            .Where(b => string.IsNullOrWhiteSpace(sTerm) || b.BookName.ToLower().Contains(sTerm));

            if (genreId > 0)
            {
                query = query.Where(b => b.GenreId == genreId);
            }

            var totalBooks = await query.CountAsync();

            var books = await query
                .OrderBy(b => b.BookName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalBooks);
        }
    }
}
