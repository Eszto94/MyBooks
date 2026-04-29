using MyBooks.BookManagementService.Domain;
using MyBooks.BookManagementService.Repositories;

namespace MyBooks.BookManagementService.Configurations;

public class BookmanagementSeeder(IBookVisibilityRepository bookVisibilityRepository)
{
    private readonly IBookVisibilityRepository bookVisibilityRepository = bookVisibilityRepository;

    public async Task SeedAsync()
    {
        if (await bookVisibilityRepository.AnyBookVisibilityAsync())
        {
            return;
        }

        var bookVisibilities = new List<BookVisibility>
        {
            new BookVisibility 
            {
                UserId = "69f21ded003d7acec43ef1ef",
                BookISBN = "978-0141182636",
                Visibility = VisibilityStatus.Public
            },
            new BookVisibility 
            {
                UserId = "69f21ded003d7acec43ef1ef",
                BookISBN = "978-0061120084",
                Visibility = VisibilityStatus.Private
            },
            new BookVisibility 
            {
                UserId = "69f21dfa99b6851df0092bda",
                BookISBN = "978-0201616224",
                Visibility = VisibilityStatus.FriendOnly
            },
            new BookVisibility 
            {
                UserId = "69f21dfa99b6851df0092bda",
                BookISBN = "978-0544003415",
                Visibility = VisibilityStatus.Public
            },
            new BookVisibility 
            {
                UserId = "69f21e02ac77e51b22b5afdb",
                BookISBN = "978-0553386790",
                Visibility = VisibilityStatus.FriendOnly
            },
            new BookVisibility 
            {
                UserId = "69f21e02ac77e51b22b5afdb",
                BookISBN = "978-0141182636",
                Visibility = VisibilityStatus.Private
            }
        };

        foreach (var bookVisibility in bookVisibilities)
        {
            await bookVisibilityRepository.AddMyBookAsync(bookVisibility);
        }
    }
}
