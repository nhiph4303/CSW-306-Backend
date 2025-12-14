using ChatApplication.Models;

namespace ChatApplication.Repository
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<Message>> GetBroadcastMessagesAsync();
        Task<List<Message>> GetMessagesByRoomAsync(string roomName);
        Task<List<Message>> GetPrivateMessagesAsync(string userId1, string userId2);
    }
}
