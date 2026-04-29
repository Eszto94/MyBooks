using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MyBooks.BookService.Domain;
using MyBooks.BookService.Repositories;
using MyBooks.Shared.Contracts;

namespace MyBooks.BookService.Controllers;

[ApiController]
[Route("books")]
public class BookController(IBookRepository bookRepository, IPublishEndpoint publish) : ControllerBase
{
    private readonly IBookRepository bookRepository = bookRepository;
    private readonly IPublishEndpoint publish = publish;

    [HttpGet(Name = "GetBooks")]
    public async Task<IActionResult> Get()
    {
        return Ok(await bookRepository.GetAllBooksAsync());
    }

    [HttpGet("{isbn}", Name = "GetBookByISBN")]
    public async Task<IActionResult> GetByISBN(string isbn)
    {
        var book = await bookRepository.GetByISBNAsync(isbn);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost(Name = "AddBook")]
    public async Task<IActionResult> AddBook(Book book)
    {
        var result = await bookRepository.AddBookAsync(book);
        return Ok(result);
    }

    [HttpPut("{isbn}", Name = "UpdateBook")]
    public async Task<IActionResult> UpdateBook(string isbn, Book book)
    {
        if (isbn != book.ISBN)
        {
            return BadRequest();
        }

        var result = await bookRepository.UpdateBookAsync(book);

        return Ok(result);
    }

    [HttpDelete("{isbn}", Name = "DeleteBook")]
    public async Task<IActionResult> DeleteBook(string isbn)
    {
        var result = await bookRepository.DeleteBookAsync(isbn);

        await publish.Publish(new DeletedBook()
        {
            ISBN = isbn,
        });

        return Ok(result);
    }
}
