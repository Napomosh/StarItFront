using StarItFront.Models.Games;

using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using StarItFront.Models;

namespace StarItFront.Pages.Games;

public partial class GameView : BasePage
{
    [Parameter]
    public long GameId { get; set; }

    private GameCardModel? Game { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadGame();
    }

    private async Task LoadGame()
    {
        isLoading = true;
        errorMessage = string.Empty;

        try
        {
            var response = await httpClient.GetAsync($"api/games/gameId={GameId}");
            
            if (response.IsSuccessStatusCode)
                Game = (await response.Content.ReadFromJsonAsync<ResponseModel<GameCardModel>>())?.Data;
            else
                errorMessage = $"Failed to load game details. Status code: {response.StatusCode}";
        }
        catch (Exception ex)
        {
            errorMessage = $"Error loading game: {ex.Message}";
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
}