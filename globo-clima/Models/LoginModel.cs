using System.Text.Json.Serialization;

namespace globo_clima.Models
{
    public class LoginModel
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
