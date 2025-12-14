using LeThiMinhTuoi_Lab7_Backend.Data;
using LeThiMinhTuoi_Lab7_Backend.Models;

namespace LeThiMinhTuoi_Lab7_Backend.Repositories
{
    public class BookRepository : IRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) { 
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            var addedBook = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return addedBook.Entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return Task.FromResult(false);
            }
            _context.Books.Remove(existingBook);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<List<Book>> GetAllAsync()
        {
            var books = _context.Books.ToList();
            return Task.FromResult(books);
        }

        public Task<Book> GetByIdAsync(int id)
        {
            var existingBook = _context.Books.Find(id);
            return Task.FromResult(existingBook);
        }

        public Task<Book> UpdateAsync(int id, Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return Task.FromResult<Book>(null);
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.Genre = book.Genre;
            _context.SaveChanges();
            return Task.FromResult(existingBook);
        }
    }
}
