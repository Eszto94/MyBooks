using MassTransit;
using MyBooks.BookManagementService.Repositories;
using MyBooks.Shared.Contracts;

namespace MyBooks.BookManagementService.Consumers;

public class DeletedBookConsumer(IBookVisibilityRepository bookVisibilityRepository) : IConsumer<DeletedBook>
{
    private readonly IBookVisibilityRepository bookVisibilityRepository = bookVisibilityRepository;

    public async Task Consume(ConsumeContext<DeletedBook> context)
    {
        await bookVisibilityRepository.DeleteVisibilitiesByBookISBN(context.Message.ISBN);
    }
}
