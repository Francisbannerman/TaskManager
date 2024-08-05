using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using TaskManager.Infrastructure;

namespace TaskManager.API.Gateway.Services;

public class GatewayServices : IGatewayServices
{
    private readonly HttpClient _http;
    public GatewayServices(HttpClient http)
    {
        _http = http;
    }
    
    public async Task AddAuthorizationHeader(string token)
    {
        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<string> GetUserId(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //var name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        return userId;
    }
    
}