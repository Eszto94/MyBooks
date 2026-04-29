using MyBooks.BookService.Domain;

namespace MyBooks.BookService.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();

    Task<Book?> GetByISBNAsync(string isbn);

    Task<bool> AddBookAsync(Book book);

    Task<bool> UpdateBookAsync(Book book);

    Task<bool> DeleteBookAsync(string isbn);
}
