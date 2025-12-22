using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagementSystem;
using BookManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using BookManagementSystem.DTO;
using BookManagementSystem.Models;

namespace BookManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context, IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
            _context = context;
        }

        // GET: api/Books
        // Get books (with genre info)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookRepo.GetAll();
            return Ok(books);
        }

        // GET: api/Books/{id}
        // get book by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepo.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        // Update book (ADMIN)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdationDto dto)
        {
            // get book from db
            var book = await _bookRepo.GetById(id);

            // if book not found, return NotFound Status
            if (book == null)
                return NotFound("Book not found");

            // handle update genre
            if (dto.GenreId != null )
            {
                if (dto.GenreId < 1)
                {
                    return BadRequest("GenreId is not valid");
                }
                var genre = await _context.Genres.FindAsync(dto.GenreId);
                if (genre == null)
                {
                    return NotFound("Genre not found");
                }
                book.Genre = genre;
            }
           
            // handle update author
            if (!string.IsNullOrEmpty(dto.Author))
            {
                book.Author = dto.Author;
            }

            // handle update title
            if (!string.IsNullOrEmpty(dto.Title))
            {
                book.Title = dto.Title;
            }


            // handle update price
            if (dto.Price.HasValue && dto.Price.Value > 0)
            {
                book.Price = dto.Price.Value;
            }

            // handle update stockQuantity
            if (dto.StockQuantity.HasValue && dto.StockQuantity.Value >= 0)
            {
                book.StockQuantity = dto.StockQuantity.Value;
            }

            await _bookRepo.Update(book);
            return Ok(book);
        }

        // POST: api/Books
        // Create book (ADMIN)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] BookCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await _context.Genres.FindAsync(dto.GenreId);

            if (genre == null)
            {
                return NotFound($"Can not find any Gerne with Id:{dto.GenreId}");
            }

            var book = new Book()
            {
                Genre = genre,
                Author = dto.Author,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Title = dto.Title,
                CreatedAt = DateTime.Now   //THÊM DÒNG NÀY
            };

            await _bookRepo.Add(book);

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                book
            );
        }

        // DELETE: /api/books/{id}
        // Delete book (ADMIN)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepo.GetById(id);
            if (book == null)
                return NotFound();

            await _bookRepo.Delete(id);
            return NoContent();
        }
    }
}
