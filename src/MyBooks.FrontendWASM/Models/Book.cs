namespace MyBooks.FrontendWASM.Models;

public class Book
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = [];
    public string Category { get; set; } = string.Empty;
}
