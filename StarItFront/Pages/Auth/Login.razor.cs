using System.Net.Http.Json;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using StarItFront.Models.Auth;
using Blazored.LocalStorage;
using StarItFront.Providers.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using StarItFront.Models;

namespace StarItFront.Pages.Auth;

public partial class Login : BasePage
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    [Inject]
    private ILocalStorageService localStorage { get; set; }
    
    [Inject]
    private AuthenticationStateProvider authStateProvider { get; set; }
    
    [Inject]
    private NavigationManager navigationManager { get; set; }


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
                var concreteAuthStateProvider = (AuthStateProvider)authStateProvider;
                concreteAuthStateProvider.NotifyUserAuthentication(jwtToken);
            }
            else
            {
                errorMessage = (await response.Content.ReadFromJsonAsync<ResponseError>())?.Error ?? "Invalid server response";
            }
        }
        catch (Exception ex)
        {
            errorMessage = "Internal server error: " + ex.Message;
        }
        
        navigationManager.NavigateTo("/");
    }
}