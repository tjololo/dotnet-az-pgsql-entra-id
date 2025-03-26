namespace dotnet_az_pgsql_entra_id.models;

public record HelloResponse
{
    public string Message { get; init; } = "";
    public DateTime ServerTime { get; init; }
}