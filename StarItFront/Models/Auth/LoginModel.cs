using System.Text.Json.Serialization;

namespace StarItFront.Models.Auth;

public class LoginModel
{
    [JsonPropertyName("jwt_token")]
    public string JwtToken { get; set; } = string.Empty;
}