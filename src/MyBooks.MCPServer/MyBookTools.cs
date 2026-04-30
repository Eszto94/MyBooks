using Microsoft.AspNetCore.Mvc;
using ModelContextProtocol.Server;
using MyBooks.MCPServer.Models;
using System.ComponentModel;

namespace MyBooks.MCPServer;

[McpServerToolType]
internal class MyBookTools
{
    [McpServerTool(Name = "GetBooks", Title = "Get all books", Destructive = false, Idempotent = true, ReadOnly = true)]
    [Description("Gets every book without any filter criteria.")]
    internal static async Task<object> GetBooksAsync([FromServices] HttpClient http)
    {
        var allBooks = await http.GetFromJsonAsync<List<Book>>("books");

        return allBooks!;
    }
    
    [McpServerTool(Name = "CreateNewBook", Title = "Create a new book", Destructive = false, Idempotent = false, ReadOnly = false)]
    [Description("Creates a new book with the given information. The ISBN must be unique. Title, authors, and category must be provided. Category must be one of the predefined options. Category options: Classic, Programming, Science, History, Fiction, Non-Fiction, Biography, Other.")]
    internal static async Task<object> CreateNewBook(string isbn, string title, List<string> authors, string category, [FromServices] HttpClient http, CancellationToken ct)
    {
        Console.WriteLine($"\n\n\nCreating a new book with ISBN: {isbn}, Title: {title}, Authors: {string.Join(", ", authors)}, Category: {category}\n\n\n");

        Book book = new Book
        {
            ISBN = isbn,
            Title = title,
            Authors = authors,
            Category = category
        };

        var response = await http.PostAsJsonAsync("books", book, ct);

        return response.StatusCode;
    }

    [McpServerTool(Name = "UpdateBook", Title = "Update book", Destructive = false, Idempotent = false, ReadOnly = false)]
    [Description("Updates an existing book with the given information. The ISBN must match the existing book's ISBN. Title, authors, and category must be provided. Category must be one of the predefined options. Category options: Classic, Programming, Science, History, Fiction, Non-Fiction, Biography, Other.")]
    internal static async Task<object> UpdateBook(string isbn, string title, List<string> authors, string category, [FromServices] HttpClient http, CancellationToken ct)
    {
        Console.WriteLine($"\n\n\nUpdating book with ISBN: {isbn}, Title: {title}, Authors: {string.Join(", ", authors)}, Category: {category}\n\n\n");

        Book book = new Book
        {
            ISBN = isbn,
            Title = title,
            Authors = authors,
            Category = category
        };

        var response = await http.PutAsJsonAsync($"books/{isbn}", book, ct);

        return response.StatusCode;
    }

    [McpServerTool(Name = "DeleteBook", Title = "Delete a book", Destructive = true, Idempotent = true, ReadOnly = false)]
    [Description("Deletes an existing book with the given ISBN.")]
    internal static async Task<object> DeleteBook(string isbn, [FromServices] HttpClient http, CancellationToken ct)
    {
        Console.WriteLine($"\n\n\nDeleting book with ISBN: {isbn}\n\n\n");

        var response = await http.DeleteAsync($"books/{isbn}", ct);

        return response.StatusCode;
    }

    [McpServerTool(Name = "GetUsers", Title = "Get all users", Destructive = false, Idempotent = true, ReadOnly = true)]
    [Description("Gets every user without any filter criteria.")]
    internal static async Task<object> GetUsersAsync([FromServices] HttpClient http)
    {
        var allUsers = await http.GetFromJsonAsync<List<User>>("users");

        return allUsers!;
    }

    [McpServerTool(Name = "GetMyBooks", Title = "Get books for user", Destructive = false, Idempotent = true, ReadOnly = true)]
    [Description("Gets the books associated for the user by ID.")]
    internal static async Task<object> GetMyBooksAsync(string userId, [FromServices] HttpClient http)
    {
        var allBooks = await http.GetFromJsonAsync<List<Book>>("books");
        var myBooks = await http.GetFromJsonAsync<List<BookVisibility>>($"bookmanagement/mybooks/{userId}");

        return new
        {
            AllBooks = allBooks,
            UserBooks = myBooks
        };
    }

    [McpServerTool(Name = "AddMyBook", Title = "Add a book for user", Destructive = false, Idempotent = false, ReadOnly = false)]
    [Description("Adds a book association for the user by ID. The ISBN must match an existing book's ISBN.")]
    internal static async Task<object> AddMyBookAsync(string userId, string isbn, [FromServices] HttpClient http, CancellationToken ct)
    {
        Console.WriteLine($"\n\n\nAdding book with ISBN: {isbn} for user: {userId}\n\n\n");

        BookVisibility myBook = new BookVisibility
        {
            UserId = userId,
            BookISBN = isbn
        };

        var response = await http.PostAsJsonAsync("bookmanagement/mybooks", myBook, ct);

        return response.StatusCode;
    }

    [McpServerTool(Name = "DeleteMyBook", Title = "Delete a book for user", Destructive = true, Idempotent = true, ReadOnly = false)]
    [Description("Deletes a book association for the user by ID. The ISBN must match an existing book's ISBN.")]
    internal static async Task<object> DeleteMyBookAsync(string userId, string isbn, [FromServices] HttpClient http, CancellationToken ct)
    {
        Console.WriteLine($"\n\n\nDeleting book with ISBN: {isbn} for user: {userId}\n\n\n");

        var response = await http.DeleteAsync($"bookmanagement/mybooks/{userId}/{isbn}", ct);

        return response.StatusCode;
    }

    [McpServerTool(Name = "GetFriends", Title = "Get all friends for user", Destructive = false, Idempotent = true, ReadOnly = true)]
    [Description("Gets all friends associated for the user by ID.")]
    internal static async Task<object> GetFriendsAsync(string userId, [FromServices] HttpClient http)
    {
        var friends = await http.GetFromJsonAsync<List<UserFriend>>($"users/friend/?userId={userId}");

        return friends!;
    }
}
