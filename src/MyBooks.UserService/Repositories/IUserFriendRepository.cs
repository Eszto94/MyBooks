using MyBooks.UserService.Domain;

namespace MyBooks.UserService.Repositories;

public interface IUserFriendRepository
{
    Task<IEnumerable<UserFriend>> GetAllUserFriendsAsync();

    Task<IEnumerable<UserFriend>> GetUserFriendsAsync(string userId);

    Task<bool> AddUserFriendAsync(UserFriend userFriend);

    Task<bool> DeleteUserFriendAsync(string id);
}

