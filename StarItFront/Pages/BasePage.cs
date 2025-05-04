using Microsoft.AspNetCore.Components;

namespace StarItFront.Pages;

public abstract class BasePage : ComponentBase
{
    [Inject] protected HttpClient httpClient { get; set; }
    
    protected string errorMessage { get; set; } = "";
}