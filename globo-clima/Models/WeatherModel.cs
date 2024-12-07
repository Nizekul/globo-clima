using System.Text.Json.Serialization;

namespace globo_clima.Models
{
    public class WeatherModel
    {
        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }
        [JsonPropertyName("main")]
        public Main Main { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Coord
    {
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }
}
