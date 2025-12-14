using LeThiMinhTuoi_Lab6_Backend.Data;
using LeThiMinhTuoi_Lab6_Backend.Interfaces;
using LeThiMinhTuoi_Lab6_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LeThiMinhTuoi_Lab6_Backend.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Message>> GetMessagesByRoomAsync(string roomName)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.RoomName == roomName && m.MessageType == 1)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Message>> GetPrivateMessagesAsync(string userId1, string userId2)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.MessageType == 2 &&
                           ((m.SenderId == userId1 && m.ReceiverId == userId2) ||
                            (m.SenderId == userId2 && m.ReceiverId == userId1)))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Message>> GetBroadcastMessagesAsync()
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.MessageType == 0)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
