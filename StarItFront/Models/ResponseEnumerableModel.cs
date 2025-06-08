using System.Text.Json.Serialization;

namespace StarItFront.Models;

public class ResponseEnumerableModel<T>
{
    [JsonPropertyName("items")] 
    public IEnumerable<T> Items { get; set; } = [];
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; } = 0;
}