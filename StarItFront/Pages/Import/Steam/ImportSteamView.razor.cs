namespace StarItFront.Pages.Import.Steam;

public partial class ImportSteamView : BasePage
{
    private async Task ImportSteamGames()
    {
        isLoading = true;
        errorMessage = string.Empty;

        try
        {
            var response = await httpClient.GetAsync("api/admin/import/steam/games");

            if (!response.IsSuccessStatusCode)
                errorMessage = $"Failed to load Steam ID. Status code: {response.StatusCode}";
        }
        catch (Exception ex)
        {
            errorMessage = $"Error loading Steam ID: {ex.Message}";
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }

    private void ImportDevelopers()
    {
        
    }
    private void ImportGenres()
    {
        
    }
    private void ImportPublishers()
    {
        
    }
}