using LeThiMinhTuoi_Lab7_Backend.Models;

namespace LeThiMinhTuoi_Lab7_Backend.Repositories
{
    public interface IRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
    }
}
