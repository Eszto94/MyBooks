using MyBooks.MCPServer.Models;

namespace MyBooks.BookService.Client;

public interface IBookApiClient
{
    Task<Book[]> GetBooksAsync(CancellationToken ct = default);

    Task<Book?> GetBookByISBNAsync(string isbn, CancellationToken ct = default);

    Task<Book> AddBookAsync(Book book, CancellationToken ct = default);

    Task<Book> UpdateBookAsync(string isbn, Book book, CancellationToken ct = default);

    Task DeleteBookAsync(string isbn, CancellationToken ct = default);
}

public class BookApiClient(HttpClient httpClient) : IBookApiClient
{
    private readonly HttpClient http = httpClient;

    public async Task<Book[]> GetBooksAsync(CancellationToken ct = default)
    {
        var response = await http
            .GetAsync("books", ct)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<Book[]>(ct)
            .ConfigureAwait(false) ?? [];
    }

    public async Task<Book?> GetBookByISBNAsync(string isbn, CancellationToken ct = default)
    {
        var response = await http
            .GetAsync($"books/{isbn}", ct)
            .ConfigureAwait(false);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<Book>(ct)
            .ConfigureAwait(false);
    }

    public async Task<Book> AddBookAsync(Book book, CancellationToken ct = default)
    {
        var response = await http
            .PostAsJsonAsync("books", book, ct)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<Book>(ct)
            .ConfigureAwait(false)
            ?? throw new InvalidOperationException("Response body was null.");
    }

    public async Task<Book> UpdateBookAsync(string isbn, Book book, CancellationToken ct = default)
    {
        var response = await http
            .PutAsJsonAsync($"books/{isbn}", book, ct)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<Book>(ct)
            .ConfigureAwait(false)
            ?? throw new InvalidOperationException("Response body was null.");
    }

    public async Task DeleteBookAsync(string isbn, CancellationToken ct = default)
    {
        var response = await http
            .DeleteAsync($"books/{isbn}", ct)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }
}
