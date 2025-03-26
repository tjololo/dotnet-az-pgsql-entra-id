using dotnet_az_pgsql_entra_id.models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_az_pgsql_entra_id.controllers;

[Route("/hello")]
[ApiController]
public class HelloController
{

    public HelloController()
    {
    }

    [HttpGet]
    public Task<HelloResponse> GetHello()
    {
        return Task.FromResult(new HelloResponse()
        {
            Message = "Hello, World!",
            ServerTime = DateTime.Now
        });
    }
}