using System.Text.Json.Serialization;

namespace StarItFront.Models;

public class ResponseModel<T>
{
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
}