
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.RegularExpressions;
using ChatApplication.Repository;
using ChatApplication.Models;

namespace ChatApplication.Chat
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IAppUserRepository _userRepository;
        private readonly ILogger<ChatHub> _logger;
        private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();
        private static readonly List<string> _rooms = new() { "1", "2", "3", "4" };

        public ChatHub(IMessageRepository messageRepository, IAppUserRepository userRepository, ILogger<ChatHub> logger)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task SendMessage(string message)
        {
            var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User.Identity.Name;
            Console.WriteLine($"SenderId: {senderId}, UserName: {userName}");
            await _messageRepository.AddMessageAsync(new Message
            {
                SenderId = senderId,
                Content = message,
                MessageType = 0, 
                CreatedAt = DateTime.Now
            });

            await Clients.All.SendAsync("ReceiveMessage", userName, message, DateTime.Now.ToString("HH:mm"));
        }

        public async Task JoinRoom(string roomName)
        {
            var userName = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("UserJoined", userName, $"{userName} joined the room.");
        }

        public async Task LeaveRoom(string roomName)
        {
            var userName = Context.User.Identity.Name;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("UserLeft", userName, $"{userName} left the room.");
        }

        public async Task SendGroupMessage(string roomName, string message)
        {
            var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User.Identity.Name;
            var time = DateTime.Now;

            _logger.LogInformation("Room {Room} | {User} | {Time} | {Message}",
                roomName, userName, time, message);

            await _messageRepository.AddMessageAsync(new Message
            {
                SenderId = senderId,
                RoomName = roomName,
                Content = message,
                MessageType = 1,
                CreatedAt = DateTime.Now
            });

            await Clients.Group(roomName).SendAsync("ReceiveGroupMessage", roomName, userName, message, DateTime.Now.ToString("HH:mm"));
        }

        public async Task SendPrivateMessage(string receiverUserName, string message)
        {
            var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var senderName = Context.User.Identity.Name;

            if (_connections.TryGetValue(receiverUserName, out var receiverConnectionId))
            {
                var receiverUser = await _userRepository.GetUserByNameAsync(receiverUserName);
                var receiverId = receiverUser?.Id;

                await _messageRepository.AddMessageAsync(new Message
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = message,
                    MessageType = 2, 
                    CreatedAt = DateTime.Now
                });

                await Clients.Client(receiverConnectionId)
                    .SendAsync("ReceivePrivateMessage", senderName, message, DateTime.Now.ToString("HH:mm"));

                await Clients.Caller
                    .SendAsync("ReceivePrivateMessage", "You", message, DateTime.Now.ToString("HH:mm"));
            }
        }


        public Task<List<string>> GetActiveUsers()
        {
            return Task.FromResult(_connections.Keys.ToList());
        }

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                _connections[userName] = Context.ConnectionId;

                await Clients.All.SendAsync("UpdateUserList", _connections.Keys.ToList());
            }

            await base.OnConnectedAsync();
        }



        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (userName != null)
            {
                _connections.Remove(userName);
                await Clients.All.SendAsync("UpdateUserList", _connections.Keys.ToList());
            }
            await base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId(string userName)
        {
            return _connections.TryGetValue(userName, out var connectionId) ? connectionId : string.Empty;
        }

        public Task<List<string>> GetRooms()
        {
            return Task.FromResult(_rooms);
        }
    }
}
