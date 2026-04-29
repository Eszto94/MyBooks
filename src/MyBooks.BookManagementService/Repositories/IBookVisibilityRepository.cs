using MyBooks.BookManagementService.Domain;

namespace MyBooks.BookManagementService.Repositories;

public interface IBookVisibilityRepository
{
    Task<bool> AnyBookVisibilityAsync();

    Task<IEnumerable<BookVisibility>> GetAllMyBooksAsync(string userId);

    Task<bool> AddMyBookAsync(BookVisibility mybook);

    Task<bool> UpdateMyBookAsync(BookVisibility mybook);

    Task<bool> DeleteMyBookAsync(string userId, string isbn);

    Task<bool> DeleteVisibilitiesByBookISBN(string isbn);
}

