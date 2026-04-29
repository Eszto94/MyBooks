using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBooks.BookManagementService.Domain;

public enum VisibilityStatus
{
    Private,
    FriendOnly,
    Public,
    ForSale
}

public class BookVisibility
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string BookISBN {  get; set; } = string.Empty;

    [BsonElement("Visibility")]
    public VisibilityStatus Visibility { get; set; }

}
