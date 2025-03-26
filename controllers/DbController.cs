using dotnet_az_pgsql_entra_id.models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace dotnet_az_pgsql_entra_id.controllers;

[Route("/db")]
[ApiController]
public class DbController
{
    private readonly NpgsqlDataSource _dataSource;
    
    public DbController(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    [HttpGet]
    public async Task<HelloResponse> Get()
    {
        await using var connection = _dataSource.CreateConnection();
        await connection.OpenAsync();
        
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT version(), now()";
        
        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();

        return new HelloResponse()
        {
            Message = reader.GetString(0),
            ServerTime = reader.GetDateTime(1)
        };
    }
}