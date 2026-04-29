using MongoDB.Driver;

namespace MyBooks.Shared.Common.DataAccess;

public interface IMongoDbConnectionFactory
{
    string ConnectionString { get; }
    string DatabaseName { get; }
    IMongoDatabase GetDatabase();
}
