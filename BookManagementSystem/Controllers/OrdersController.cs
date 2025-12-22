using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagementSystem;
using BookManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookManagementSystem.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace BookManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ToListAsync();

            return Ok(orders);
        }


        // GET
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int userId)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            int jwtUserId = int.Parse(userIdClaim.Value);

            // must match JWT
            if (userId != jwtUserId)
                return Forbid();

            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .Where(o=> o.User.Id == userId)
                .ToListAsync();

            return Ok(orders);
        }


        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1. Lấy userId từ JWT
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            // get user form database
            var user  = await _context.Users.FindAsync(userId);
            // check if user exists or not
            if (user == null)
            {
                return NotFound("User not found");
            }

            // 2. Tạo Order
            var order = new Order
            {
                User = user,
                OrderDate = DateTime.Now,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var book = await _context.Books.FindAsync(item.BookId);

                // Validate book exists
                if (book == null)
                    return NotFound($"Book {item.BookId} not found");

                // Validate quantity > 0
                if (item.Quantity <= 0)
                    return BadRequest("Quantity must be greater than 0");

                // Validate stock
                if (book.StockQuantity < item.Quantity)
                    return BadRequest($"Not enough stock for book {book.Title}");

                var orderItem = new OrderItem
                {
                    Order = order,
                    Book = book,
                    Quantity = item.Quantity,
                    UnitPrice = book.Price,
                    CreatedAt = DateTime.Now
                };

                totalAmount += item.Quantity * book.Price;
                book.StockQuantity -= item.Quantity;

                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
