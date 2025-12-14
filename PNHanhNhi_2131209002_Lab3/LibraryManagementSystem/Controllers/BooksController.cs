using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = [
                new Book() { Author = "Xuân Quỳnh", Genre = "Thơ", Title = "Sóng"},
                new Book() { Author = "Vũ Trọng Phụng", Genre = "Tiểu thuyết", Title = "Số đỏ"},
                new Book() { Author = "Nguyễn Quang Sáng", Genre = "Truyện ngắn", Title = "Chiếc lược ngà"},
            ];


        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(string id)
        {
            Book? book = books.FirstOrDefault(b => b.Id.Equals(id, StringComparison.OrdinalIgnoreCase), null);
            return book != null ? Ok(book) : NotFound();
        }

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] BookCreationDTO requestBody)
        {
            Book newBook = new()
            {
                Author = requestBody.Author,
                Genre = requestBody.Genre,
                Title = requestBody.Title,
            };

            books.Add(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook }, newBook);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(string id, [FromBody] BookUpdationDTO requestBody)
        {
            Book? book = books.FirstOrDefault(b => b.Id.Equals(id, StringComparison.OrdinalIgnoreCase), null);

            if (book == null)
            {
                return NotFound();
            }

            book.Author = String.IsNullOrEmpty(requestBody.Author) ? book.Author : requestBody.Author;
            book.Genre = String.IsNullOrEmpty(requestBody.Genre) ? book.Genre: requestBody.Genre;
            book.Title = String.IsNullOrEmpty(requestBody.Title) ? book.Title : requestBody.Title;

            return CreatedAtAction(nameof(GetBookById), new { id = id }, book);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(string id)
        {
            Book? book = books.FirstOrDefault(b => b.Id.Equals(id, StringComparison.OrdinalIgnoreCase), null);

            if (book == null)
            {
                return NotFound();
            }

            books.Remove(book);

            return NoContent();
        }
    }
}
