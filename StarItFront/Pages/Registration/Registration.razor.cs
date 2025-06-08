using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using StarItFront.Models;
using StarItFront.Models.Auth;
using StarItFront.Models.Registration;
using StarItFront.Providers.Auth;

namespace StarItFront.Pages.Registration;

public partial class Registration : BasePage
{
    [Inject]
    private NavigationManager navigationManager { get; set; }
    [Inject]
    private ILocalStorageService localStorage { get; set; }
    [Inject]
    private AuthStateProvider authStateProvider { get; set; }
    private RegistrationModel registrationModel = new();
  
    private bool registrationSuccessful = false;

    private async Task DoRegistration()
    {
        errorMessage = string.Empty;
        registrationSuccessful = false;
        
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/register", new
            {
                registrationModel.Email,
                registrationModel.Nickname,
                registrationModel.Password
            });

            if (response.IsSuccessStatusCode)
            {
                registrationSuccessful = true;
                
                var result = await response.Content.ReadFromJsonAsync<LoginModel>();
                var jwtToken = result?.JwtToken;
                if (jwtToken != null)
                {
                    await localStorage.SetItemAsync("jwt_token", jwtToken);
                    authStateProvider.NotifyUserAuthentication(jwtToken);
                }
            }
            else
            {
                var errorContent = (await response.Content.ReadFromJsonAsync<ResponseError>())?.Error;
                errorMessage = string.IsNullOrEmpty(errorContent) 
                    ? $"Registration failed: {response.StatusCode}" 
                    : errorContent;
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"Internal server error: {ex.Message}";
        }
        
        navigationManager.NavigateTo("/");
    }
}