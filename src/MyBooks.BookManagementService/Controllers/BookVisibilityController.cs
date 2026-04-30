using Microsoft.AspNetCore.Mvc;
using MyBooks.BookManagementService.Domain;
using MyBooks.BookManagementService.Repositories;

namespace MyBooks.BookManagementService.Controllers;

[ApiController]
[Route("bookmanagement/mybooks")]
public class BookVisibilityController(IBookVisibilityRepository bookVisibilityRepository) : ControllerBase
{
    private readonly IBookVisibilityRepository bookVisibilityRepository = bookVisibilityRepository;

    [HttpGet("{userId}", Name = "GetMyBook")]
    public async Task<IActionResult> Get(string userId)
    {
        return Ok(await bookVisibilityRepository.GetAllMyBooksAsync(userId));
    }

    [HttpGet(Name = "GetPublicBooks")]
    public async Task<IActionResult> Get()
    {
        return Ok(await bookVisibilityRepository.GetAllPublicBooksAsync());
    }

    [HttpPost(Name = "AddMyBook")]
    public async Task<IActionResult> AddMyBook(BookVisibility mybook)
    {
        await bookVisibilityRepository.AddMyBookAsync(mybook);

        return Ok();
    }

    [HttpPut("{id}", Name = "UpdateMyBook")]
    public async Task<IActionResult> UpdateMyBook(string id, BookVisibility mybook)
    {
        if (id != mybook.Id)
        {
            return BadRequest();
        }

        await bookVisibilityRepository.UpdateMyBookAsync(mybook);

        return Ok();
    }

    [HttpDelete("{userId}/{isbn}", Name = "DeleteMYBook")]
    public async Task<IActionResult> DeleteMyBook(string userId, string isbn)
    {
        await bookVisibilityRepository.DeleteMyBookAsync(userId, isbn);

        return Ok();
    }
}
