using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Technical_Assessment.Application.Strategies;
using Technical_Assessment.Core.Entities;
using Technical_Assessment.Core.Repositories;
using Technical_Assessment.Infrastructure.Data;
using Technical_Assessment.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SMSDb");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(_ => new SqlConnection(connectionString));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<SMSRepository>();

var app = builder.Build();

await EnsureDb(app.Services);

app.MapGet("/", () => "Technical Assessment API");

app.MapGet("/sms", async (SMSRepository repository) =>
{
    return await repository.GetAllAsync();
});

app.MapGet("/sms/{id}", async (int id, SMSRepository repository) =>
{
    return await repository.GetByIdAsync(id);
});

app.MapPost("/sms", async (SMS sms, SMSRepository repository) =>
{
    SMSVendorClient client = new(sms);
    string message = await client.Send(sms, repository);
    if (message != "Ok") return Results.Problem(message, statusCode: 500);
    return Results.Created($"/sms/{sms.ID}", sms);
});

app.MapDelete("/sms/{id}", async (int id, SMSRepository repository) =>
{
    await repository.DeleteAsync(id);
    return Results.Ok($"SMS with id: {id} deleted successfully");
});

app.MapPut("/sms", async (SMS sms, SMSRepository repository) =>
{
    await repository.UpdateAsync(sms);
    return Results.Created($"/sms/{sms.ID}", sms);
});

app.Run("http://localhost:3000");


async Task EnsureDb(IServiceProvider services)
{
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<SqlConnection>();
    var sql = $@"IF OBJECT_ID(N'SMSs', N'U') is null
    CREATE TABLE SMSs (
        {nameof(SMS.ID)} int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        {nameof(SMS.PhoneNumber)} varchar(15) NOT NULL,
        {nameof(SMS.Message)} nvarchar(480) NOT NULL
    );";
    await db.ExecuteAsync(sql);
}