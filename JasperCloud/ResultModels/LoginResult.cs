using System.Text.Json.Serialization;

namespace JasperCloud.ResultModels;

public class LoginResult
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}