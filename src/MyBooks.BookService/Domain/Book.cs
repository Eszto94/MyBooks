using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBooks.BookService.Domain;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]

    public string ISBN { get; set; } = null!;

    [BsonElement("Name")]
    public string Title { get; set; } = null!;

    public List<string> Authors { get; set; } = new List<string>();

    public string Category { get; set; } = null!;
}
