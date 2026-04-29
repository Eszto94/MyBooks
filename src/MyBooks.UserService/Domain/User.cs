using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBooks.UserService.Domain;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    public string Name { get; set; } = null!;

}
