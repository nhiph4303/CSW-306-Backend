using BookManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        // add book
        public async Task Add(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        // delete book
        public async Task Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        // get all book
        public async Task<List<Book>> GetAll()
        {
            return await _context.Books
                .Include(b => b.Genre)
                .ToListAsync();
        }

        // get book by genre id
        public async Task<List<Book>> GetByGenreId(int genreId)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Where(b => b.Genre.Id == genreId)
                .ToListAsync();
        }

        // get book by id
        public async Task<Book?> GetById(int id)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // update book
        public async Task Update(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
