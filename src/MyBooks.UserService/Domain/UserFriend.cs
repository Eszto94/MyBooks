using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBooks.UserService.Domain;

public class UserFriend
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    public string UserId { get; set; } = null!;

    public string FriendId { get; set; } = null!;

}
