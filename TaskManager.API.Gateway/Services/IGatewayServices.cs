namespace TaskManager.API.Gateway.Services;

public interface IGatewayServices
{
    Task AddAuthorizationHeader(string token);
    Task<string> GetUserId(string token);
}