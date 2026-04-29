using MyBooks.UserService.Domain;

namespace MyBooks.UserService.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<bool> AddUserAsync(User user);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(string userId);

        Task<User?> GetUserByNameAsync(string userName);
    }
}

