using MyBooks.BookService.Domain;
using MyBooks.BookService.Repositories;

namespace MyBooks.BookService.Configurations;

public class BookSeeder(IBookRepository bookRepository)
{
    private readonly IBookRepository bookRepository = bookRepository;

    public async Task SeedAsync()
    {
        var existing = await bookRepository.GetAllBooksAsync();

        if (existing.Any())
        {
            return;
        }

        var books = new List<Book>
        {
            new Book
            {
                ISBN = "978-0141182636",
                Title = "The Great Gatsby",
                Authors = new List<string> { "F. Scott Fitzgerald" },
                Category = "Classic"
            },
            new Book
            {
                ISBN = "978-0061120084",
                Title = "Clean Code",
                Authors = new List<string> { "Robert C. Martin" },
                Category = "Programming"
            },
            new Book
            {
                ISBN = "978-0201616224",
                Title = "The Pragmatic Programmer",
                Authors = new List<string> { "Andrew Hunt", "David Thomas" },
                Category = "Programming"
            },
            new Book
            {
                ISBN = "978-0544003415",
                Title = "The Lord of the Rings",
                Authors = new List<string> { "J.R.R. Tolkien" },
                Category = "Fantasy"
            },
            new Book
            {
                ISBN = "978-0553386790",
                Title = "A Game of Thrones",
                Authors = new List<string> { "George R.R. Martin" },
                Category = "Fantasy"
            }
        };

        foreach (var book in books)
        {
            await bookRepository.AddBookAsync(book);
        }
    }
}