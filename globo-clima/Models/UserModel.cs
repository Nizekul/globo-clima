using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace globo_clima.Models
{
    public class UserModel
    {
        [JsonIgnore]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("senha")]
        public string Senha { get; set; }
    }
}
