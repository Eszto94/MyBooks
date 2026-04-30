namespace MyBooks.MCPServer.Models;

public enum VisibilityStatus
{
    Private,
    FriendOnly,
    Public,
    ForSale
}

public class BookVisibility
{
    public string? Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string BookISBN { get; set; } = string.Empty;

    public VisibilityStatus Visibility { get; set; }

}
