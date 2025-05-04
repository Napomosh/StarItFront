using System.Text.Json.Serialization;

namespace StarItFront.Common;

public class PaginatedResponse<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; } = [];
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}