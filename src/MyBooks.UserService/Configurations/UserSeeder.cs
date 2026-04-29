using MyBooks.UserService.Domain;
using MyBooks.UserService.Repositories;

namespace MyBooks.UserService.Configurations;

public class UserSeeder(IUserRepository userRepository, IUserFriendRepository userFriendRepository)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly IUserFriendRepository userFriendRepository = userFriendRepository;

    public async Task SeedAsync()
    {
        var existing = await userRepository.GetAllUsersAsync();

        if (existing.Any())
        {
            return;
        }

        var users = new List<User>
        {
            new User { Id = "69f21ded003d7acec43ef1ef", Name = "Alice" },
            new User { Id = "69f21dfa99b6851df0092bda", Name = "Bob" },
            new User { Id = "69f21e02ac77e51b22b5afdb", Name = "Charlie" }
        };

        foreach (var user in users)
        {
            await userRepository.AddUserAsync(user);
        }

        var userFriends = new List<UserFriend>
        {
            new UserFriend { UserId = "69f21ded003d7acec43ef1ef", FriendId = "69f21dfa99b6851df0092bda" },
            new UserFriend { UserId = "69f21ded003d7acec43ef1ef", FriendId = "69f21e02ac77e51b22b5afdb" },
            new UserFriend { UserId = "69f21dfa99b6851df0092bda", FriendId = "69f21e02ac77e51b22b5afdb" }
        };

        foreach (var userFriend in userFriends)
        {
            await userFriendRepository.AddUserFriendAsync(userFriend);
        }
    }
}
