
using ChatApplication.Data;
using ChatApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ChatApplication.Models;

namespace ChatApplication.Controllers
{
    // the [Authorize] is not done here because i check the jwt in the Index.cshmtl for the connection 
    [Route("chat")]
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMessageRepository _messageRepository;
        public ChatController(AppDbContext context, IMessageRepository messageRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
        }

        [HttpGet("Index")]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Chat/Index.cshtml");
        }

        [HttpGet("Group")]
        public IActionResult Group()
        {
            return View("~/Views/Chat/Group.cshtml");
        }

        [HttpGet("Private")]
        public IActionResult Private()
        {
            return View("~/Views/Chat/Private.cshtml");
        }

        // action methods
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var messages = await _context.Messages.ToListAsync();
            return Ok(messages);
        }

        [HttpGet("messages/broadcast")]
        public async Task<ActionResult<List<Message>>> GetBroadcastMessages()
        {
            var messages = await _messageRepository.GetBroadcastMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("messages/room/{roomName}")]
        public async Task<ActionResult<List<Message>>> GetRoomMessages(string roomName)
        {
            var messages = await _messageRepository.GetMessagesByRoomAsync(roomName);
            return Ok(messages);
        }

        [HttpGet("messages/private")]
        public async Task<IActionResult> GetPrivateMessages(string receiver)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var receiverUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == receiver);
            if (receiverUser == null) return NotFound("Receiver not found");

            var messages = await _messageRepository.GetPrivateMessagesAsync(currentUserId, receiverUser.Id);

            var result = messages.Select(m => new
            {
                m.Content,
                m.CreatedAt,
                SenderName = m.SenderId == currentUserId ? "You" : m.Sender.UserName
            });

            return Ok(result);
        }


    }
}
