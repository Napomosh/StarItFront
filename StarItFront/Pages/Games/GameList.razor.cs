using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StarItFront.Common;
using StarItFront.Models.Games;

namespace StarItFront.Pages.Games;

public partial class GameList : BasePage, IAsyncDisposable
{
    [Inject] 
    private IJSRuntime js { get; set; }
    [Inject] 
    private NavigationManager navigationManager { get; set; }
    
    private List<GameCardModel> games = [];
    private int currentPage = 1;
    private readonly int pageSize = 20;
    private bool hasMorePages = true;
    private DotNetObjectReference<GameList>? objRef;
    
    [JSInvokable]
    public async Task OnScrollToBottom()
    {
        await LoadMoreGames();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            objRef = DotNetObjectReference.Create(this);
            await js.InvokeVoidAsync("initScrollListener", objRef);
            currentPage = 1;
            await LoadMoreGames();
        }
    }
    
    private async Task LoadMoreGames()
    {
        if (isLoading || !hasMorePages)
            return;

        isLoading = true;
        try
        {
            var response = await httpClient.GetAsync($"api/games?page={currentPage}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PaginatedResponse<GameCardModel>>();
                if (result is not null)
                {
                    games.AddRange(result.Items);
                    hasMorePages = result.TotalPages > currentPage;
                    currentPage++;
                }
            }
        }
        catch (Exception ex)
        {
            errorMessage = "Error loading games: " + ex.Message;
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }

    private void OnGameClick(long id)
    {
        navigationManager.NavigateTo($"/games/{id}");
    }
    
    public async ValueTask DisposeAsync()
    {
        if (objRef == null) 
            return;
        await js.InvokeVoidAsync("removeScrollListener");
        objRef.Dispose();
        objRef = null;
    }
}