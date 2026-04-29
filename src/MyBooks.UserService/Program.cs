using MyBooks.Shared.Common.DataAccess;
using MyBooks.Shared.Common.Setups;
using MyBooks.UserService.Configurations;
using MyBooks.UserService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddDefaultLogging("UserService");

builder.Services.AddSingleton<IMongoDbConnectionFactory, MongoDbConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserFriendRepository, UserFriendRepository>();
builder.Services.AddScoped<UserSeeder>();

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
    var seeder = scope.ServiceProvider.GetRequiredService<UserSeeder>();
    await seeder.SeedAsync();
}

app.Run();
