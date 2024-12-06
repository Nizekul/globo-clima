namespace globo_clima.Models
{
    public class CountryModel
    {
        public Name Name { get; set; }
        public string Region { get; set; }
        public IDictionary<string, string> Languages { get; set; }

    }
    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
}
