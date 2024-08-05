using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManager.Client.Pages;
using Task = System.Threading.Tasks.Task;


namespace TaskManager.Client.AuthService;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private readonly NavigationManager _navigationManager;

    public AuthService(HttpClient http, IJSRuntime js, NavigationManager navigationManager)
    {
        _http = http;
        _js = js;
        _navigationManager = navigationManager;
    }

    public async Task<bool> CreateAccount(Home.UserModel userModel)
    {
        var response = await _http.PostAsJsonAsync("http://localhost:5242/api/UserGateway/AddUser", userModel);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine(response.Content.ReadAsStringAsync());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);

            return true;
        }
        return false;
    }
    
    
    public async Task<bool> Login(Login.LoginUser userModel)
    {
        var response = await _http.PostAsJsonAsync("http://localhost:5242/api/UserGateway/Login/Login", userModel);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
            
            return true;
        }
        return false;
    }
    
    public async Task<string> GetToken()
    {
        return await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    public async Task<Guid> GetUserId()
    {
        var token = await GetToken();
        if (token == null)
        {
            return Guid.Empty;
        }
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "priorityId" || claim.Type == "sub").Value;
        return Guid.Parse(userId);
    }

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "token");
        _navigationManager.NavigateTo("/login", forceLoad:true);
    }

    public async Task AddAuthorizationHeader()
    {
        var token = await GetToken();
        if (token != null)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}