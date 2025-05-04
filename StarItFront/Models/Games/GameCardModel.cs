using System.Text.Json.Serialization;

namespace StarItFront.Models.Games;

public class GameCardModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("rate")]
    public short Rate { get; set; } = 0;
    
    [JsonPropertyName("images_path")]
    public string ImagesPath { get; set; } = string.Empty;
    
    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; } = DateTime.Now;
    
    [JsonPropertyName("developers")]
    public List<int> Developers { get; set; } = [];
    
    [JsonPropertyName("publishers")]
    public List<int> Publishers { get; set; } = [];
    
    [JsonPropertyName("genres")]
    public List<int> Genre { get; set; } = [];
    
    [JsonPropertyName("distributors")]
    public List<int> Distributor { get; set; } = [];
    
    [JsonPropertyName("steam_id")]
    public long? SteamId { get; set; } = null;
}