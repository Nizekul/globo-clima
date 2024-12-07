using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace globo_clima.Models
{
    public class CountryModel
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }
        [JsonPropertyName("cca2")]
        public string Cca2 { get; set; }
        [JsonPropertyName("capital")]
        public List<string> Capital { get; set; }
        [JsonPropertyName("temp_c")]
        public string TempC { get; set; }
        [JsonPropertyName("temp_f")]
        public string TempF { get; set; }
        [JsonPropertyName("latlng")]
        public List<double> LatLng { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
    }
}
