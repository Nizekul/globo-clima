using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace globo_clima.Models
{
    public class FavoritesModel
    {
        [JsonPropertyName("id")]
        [SwaggerSchema(Description = "ID do favorito. Gerado automaticamente pelo sistema.", ReadOnly = true)]
        public Guid id { get; set; }
        [JsonPropertyName("user_id")]
        [SwaggerSchema(Description = "ID do Usuario. Gerado automaticamente pelo sistema.", ReadOnly = true)]
        public Guid UserId { get; set; }  
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lon")]
        public string Lon { get; set; }
        [JsonPropertyName("temp_c")]
        public string? TempC { get; set; }
        [JsonPropertyName("temp_f")]
        public string? TempF { get; set; }
    }
}
