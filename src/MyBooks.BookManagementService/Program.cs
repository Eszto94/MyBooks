using MassTransit;
using MyBooks.BookManagementService.Configurations;
using MyBooks.BookManagementService.Consumers;
using MyBooks.BookManagementService.Repositories;
using MyBooks.Shared.Common.DataAccess;
using MyBooks.Shared.Common.Setups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddDefaultLogging("BookManagementService");

builder.Services.AddSingleton<IMongoDbConnectionFactory, MongoDbConnectionFactory>();
builder.Services.AddScoped<IBookVisibilityRepository, BookVisibilityRepository>();
builder.Services.AddScoped<BookmanagementSeeder>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DeletedBookConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<BookmanagementSeeder>();
    await seeder.SeedAsync();
}

app.Run();
