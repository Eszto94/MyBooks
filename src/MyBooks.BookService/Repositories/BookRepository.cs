using MongoDB.Driver;
using MyBooks.BookService.Domain;
using MyBooks.Shared.Common.DataAccess;

namespace MyBooks.BookService.Repositories;

public class BookRepository(IMongoDbConnectionFactory dbFactory) : IBookRepository
{
    protected IMongoCollection<Book> Collection => dbFactory.GetDatabase().GetCollection<Book>("Books");

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<Book?> GetByISBNAsync(string isbn)
    {
        return await Collection.Find(x => x.ISBN == isbn).FirstOrDefaultAsync();
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        var result = false;
        
        if (!await Collection.Find(x => x.ISBN == book.ISBN).AnyAsync())
        {
            await Collection.InsertOneAsync(book);
            result = true;
        }

        return result;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var result = false;
        
        if (await Collection.Find(x => x.ISBN == book.ISBN).AnyAsync())
        {
            await Collection.ReplaceOneAsync(x => x.ISBN == book.ISBN, book);
            result = true;
        }

        return result;
    }

    public async Task<bool> DeleteBookAsync(string isbn)
    {
        var result = false;
        
        if (await Collection.Find(x => x.ISBN == isbn).AnyAsync())
        {
            await Collection.DeleteOneAsync(x => x.ISBN == isbn);
            result = true;
        }

        return result;
    }
}
