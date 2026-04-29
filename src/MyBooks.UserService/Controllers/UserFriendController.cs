using Microsoft.AspNetCore.Mvc;
using MyBooks.UserService.Domain;
using MyBooks.UserService.Repositories;

namespace MyBooks.UserService.Controllers;

[ApiController]
[Route("users/friend")]
public class UserFriendController(IUserFriendRepository userFriendRepository) : ControllerBase
{
    private readonly IUserFriendRepository userFriendRepository = userFriendRepository;

    [HttpGet("all", Name = "GetAllUserFriends")]
    public async Task<IActionResult> GetAllUserFriends()
    {
        return Ok(await userFriendRepository.GetAllUserFriendsAsync());
    }

    [HttpGet(Name = "GetUserFriends")]
    public async Task<IActionResult> GetUserFriends(string userId)
    {
        return Ok(await userFriendRepository.GetUserFriendsAsync(userId));
    }

    [HttpPost(Name = "AddUserFriend")]
    public async Task<IActionResult> AddUserFriend(UserFriend userFriend)
    {
        return Ok(await userFriendRepository.AddUserFriendAsync(userFriend));
    }

    [HttpDelete("{id}", Name = "DeleteUserFriend")]
    public async Task<IActionResult> DeleteUserFriend(string id)
    {
        return Ok(await userFriendRepository.DeleteUserFriendAsync(id));
    }
}
