namespace MyBooks.MCPServer.Models;

public class UserFriend
{
    public string? Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FriendId { get; set; } = string.Empty;
}
