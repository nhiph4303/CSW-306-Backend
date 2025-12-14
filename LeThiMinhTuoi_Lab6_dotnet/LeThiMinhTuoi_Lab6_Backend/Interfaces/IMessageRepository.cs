using LeThiMinhTuoi_Lab6_Backend.Models;

namespace LeThiMinhTuoi_Lab6_Backend.Interfaces
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
