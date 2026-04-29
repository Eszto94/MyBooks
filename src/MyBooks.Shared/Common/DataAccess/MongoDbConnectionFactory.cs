using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MyBooks.Shared.Common.DataAccess;

public class MongoDbConnectionFactory(IConfiguration configuration) : IMongoDbConnectionFactory, IDisposable
{
    private bool disposedValue;

    public string ConnectionString => configuration["MongoDb:ConnectionString"] ?? throw new InvalidOperationException("MongoDB connection string is not configured.");

    public string DatabaseName => configuration["MongoDb:DatabaseName"] ?? throw new InvalidOperationException("MongoDB database is not set.");

    public IMongoDatabase? Database { get; private set; }

    public IMongoDatabase GetDatabase()
    {
        if (Database == null)
        {
            var client = new MongoClient(ConnectionString);
            Database = client.GetDatabase(DatabaseName);
        }

        return Database;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Database = null;
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
