using MongoDB.Driver;
using MyBooks.BookManagementService.Domain;
using MyBooks.Shared.Common.DataAccess;

namespace MyBooks.BookManagementService.Repositories;

public class BookVisibilityRepository(IMongoDbConnectionFactory dbFactory) : IBookVisibilityRepository
{
    protected IMongoCollection<BookVisibility> MyCollection => dbFactory.GetDatabase().GetCollection<BookVisibility>("Visibilities");

    public async Task<bool> AnyBookVisibilityAsync()
    {
        return await MyCollection.Find(_ => true).AnyAsync();
    }

    public async Task<IEnumerable<BookVisibility>> GetAllMyBooksAsync(string userId)
    {
        return await MyCollection.Find(x => x.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<BookVisibility>> GetAllPublicBooksAsync()
    {
        return await MyCollection.Find(x => x.Visibility == VisibilityStatus.Public).ToListAsync();
    }

    public async Task<bool> AddMyBookAsync(BookVisibility mybook)
    {
        var exists = await MyCollection
            .Find(x => x.UserId == mybook.UserId && x.BookISBN == mybook.BookISBN)
            .AnyAsync();

        if (exists)
        {
            return false;
        }

        await MyCollection.InsertOneAsync(mybook);

        return true;
    }

    public async Task<bool> UpdateMyBookAsync(BookVisibility mybook)
    {
        var exists = await MyCollection
            .Find(x => x.UserId == mybook.UserId && x.BookISBN == mybook.BookISBN)
            .AnyAsync();

        if (!exists)
        {
            return false;
        }

        await MyCollection.ReplaceOneAsync(x => x.UserId == mybook.UserId && x.BookISBN == mybook.BookISBN, mybook);

        return true;
    }

    public async Task<bool> DeleteMyBookAsync(string userId, string isbn)
    {
        var result = false;

        if (await MyCollection.Find(x => x.UserId == userId && x.BookISBN == isbn).AnyAsync())
        {
            await MyCollection.DeleteOneAsync(x => x.UserId == userId && x.BookISBN == isbn);
            result = true;
        }

        return result;
    }

    public async Task<bool> DeleteVisibilitiesByBookISBN(string isbn)
    {
        var result = false;

        if (await MyCollection.Find(x => x.BookISBN == isbn).AnyAsync())
        {
            await MyCollection.DeleteManyAsync(x => x.BookISBN == isbn);
            result = true;
        }

        return result;
    }
}
