using MongoDB.Driver;
using MyBooks.UserService.Domain;
using MyBooks.Shared.Common.DataAccess;

namespace MyBooks.UserService.Repositories;

public class UserRepository(IMongoDbConnectionFactory dbFactory) : IUserRepository
{
    protected IMongoCollection<User> Collection => dbFactory.GetDatabase().GetCollection<User>("Users");

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<bool> AddUserAsync(User user)
    {
        var result = false;
        
        if (!await Collection.Find(x => x.Id == user.Id).AnyAsync())
        {
            if (!await Collection.Find(x => x.Name == user.Name).AnyAsync())
            {
                await Collection.InsertOneAsync(user);
                result = true;
            }
        }

        return result;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var result = false;
        
        if (await Collection.Find(x => x.Id == user.Id).AnyAsync())
        {
            if (!await Collection.Find(x => x.Name == user.Name).AnyAsync())
            {
                await Collection.ReplaceOneAsync(x => x.Id == user.Id, user);
                result = true;
            }
        }

        return result;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var result = false;
        
        if (await Collection.Find(x => x.Id == userId).AnyAsync())
        {
            await Collection.DeleteOneAsync(x => x.Id == userId);
            result = true;
        }

        return result;
    }

    public async Task<User?> GetUserByNameAsync(string userName)
    {
        return await Collection.Find(x => x.Name == userName).FirstOrDefaultAsync();
    }
}
