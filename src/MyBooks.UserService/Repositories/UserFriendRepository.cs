using MongoDB.Driver;
using MyBooks.UserService.Domain;
using MyBooks.Shared.Common.DataAccess;

namespace MyBooks.UserService.Repositories;

public class UserFriendRepository(IMongoDbConnectionFactory dbFactory) : IUserFriendRepository
{
    protected IMongoCollection<UserFriend> Collection => dbFactory.GetDatabase().GetCollection<UserFriend>("UserFriends");

    public async Task<IEnumerable<UserFriend>> GetAllUserFriendsAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<UserFriend>> GetUserFriendsAsync(string userId)
    {
        return await Collection.Find(x => x.UserId == userId || x.FriendId == userId).ToListAsync();
    }

    public async Task<bool> AddUserFriendAsync(UserFriend userFriend)
    {
        var result = false;

        if (!await Collection.Find(x => x.UserId == userFriend.UserId && x.FriendId == userFriend.FriendId).AnyAsync())
        {
            await Collection.InsertOneAsync(userFriend);
            result = true;
        }

        return result;
    }

    public async Task<bool> DeleteUserFriendAsync(string id)
    {
        var result = false;

        if (await Collection.Find(x => x.Id == id).AnyAsync())
        {
            await Collection.DeleteOneAsync(x => x.Id == id);
            result = true;
        }

        return result;
    }
}
