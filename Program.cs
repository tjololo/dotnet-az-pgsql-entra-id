using Azure.Core;
using Azure.Identity;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("db");
NpgsqlConnectionStringBuilder csb = new(connStr);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.AddNpgsqlDataSource("db", configureDataSourceBuilder: (dataSourceBuilder) =>
{
    if (string.IsNullOrEmpty(dataSourceBuilder.ConnectionStringBuilder.Password))
    {
        dataSourceBuilder.UsePeriodicPasswordProvider(async (_, ct) =>
        {
            var credentials = new DefaultAzureCredential();
            var token = await credentials.GetTokenAsync(new TokenRequestContext(["https://ossrdbms-aad.database.windows.net/.default"]), ct);
            return token.Token;
        }, TimeSpan.FromMinutes(10), TimeSpan.FromSeconds(10));
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

