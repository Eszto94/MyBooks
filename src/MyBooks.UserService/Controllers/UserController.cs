using Microsoft.AspNetCore.Mvc;
using MyBooks.UserService.Domain;
using MyBooks.UserService.Repositories;

namespace MyBooks.UserService.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository userRepository = userRepository;


    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        return Ok(await userRepository.GetAllUsersAsync());
    }

    [HttpPost(Name = "AddUser")]
    public async Task<IActionResult> AddUser(User user)
    {
        var result = await userRepository.AddUserAsync(user);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateUser")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        var result = await userRepository.UpdateUserAsync(user);
        return Ok(result);
    }

    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await userRepository.DeleteUserAsync(userId);
        return Ok(result);
    }

    [HttpGet("by-name/{userName}")]
    public async Task<IActionResult> GetUserByName(string userName)
    {
        var result = await userRepository.GetUserByNameAsync(userName);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
