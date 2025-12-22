using BookManagementSystem.Models;

namespace BookManagementSystem.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task Add(Book book);
        Task Update(Book book);
        Task Delete (int id);
        Task<List<Book>> GetByGenreId(int genreId);

    }
}
