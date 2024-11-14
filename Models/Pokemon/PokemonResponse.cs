using Challenge7Days.Models.Common;

namespace Challenge7Days.Models
{
    public class PokemonResponse
    {
        public int Count { get; set; }
        public string Next { get; set; } = string.Empty;
        public string Previous { get; set; } = string.Empty;
        public IList<GenericInfo> Results { get; set; } = new List<GenericInfo>();
    }
}