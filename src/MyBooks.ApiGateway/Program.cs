using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json")
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration["FrontendApp:Url"]!)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer("Keycloak", opts =>
//    {
//        opts.Authority = "http://localhost:8080/realms/CarBookingMicroservice";
//        opts.RequireHttpsMetadata = false;
//        opts.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            RoleClaimType = "roles"
//        };
//    });

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("AllowLocalhost");
//app.UseAuthentication();
//app.UseAuthorization();

await app.UseOcelot();

app.Run();
