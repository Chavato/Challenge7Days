namespace Challenge7Days.Models
{
    public class User
    {
        public string Name { get; set; }
        List<Pokemon> PokemonsAdopted { get; set; } = new List<Pokemon>();
    }
}