using System.Text.Json.Serialization;

namespace StarItFront.Models;

public class ResponseError
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
}