using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using StarItFront.Models.Auth;
using Blazored.LocalStorage;
using StarItFront.Providers.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace StarItFront.Pages.Auth;

public partial class Login(ILocalStorageService localStorage
    , AuthenticationStateProvider authStateProvider
    , NavigationManager navigationManager) : BasePage
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    private readonly ILocalStorageService localStorage = localStorage;
    private readonly AuthStateProvider authStateProvider = (AuthStateProvider)authStateProvider;
    private readonly NavigationManager navigationManager = navigationManager;


    private async Task DoLogin()
    {
        errorMessage = string.Empty;
        var loginData = new { Email, Password };
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/login", loginData);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginModel>();
                var jwtToken = result?.JwtToken;
                if (jwtToken == null)
                {
                    errorMessage = "Invalid server response";
                    return;
                }
                await localStorage.SetItemAsync("jwt_token", jwtToken);
                authStateProvider.NotifyUserAuthentication(jwtToken);
            }
            else
            {
                errorMessage = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            errorMessage = "Internal server error: " + ex.Message;
        }
        
        navigationManager.NavigateTo("/");
    }
}