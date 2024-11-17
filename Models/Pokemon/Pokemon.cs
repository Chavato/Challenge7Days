using Challenge7Days.Models.Features;

namespace Challenge7Days.Models
{
    public class Pokemon
    {
        public IList<AbilityInfo> Abilities { get; set; } = new List<AbilityInfo>();
        public IList<StatInfo> Stats { get; set; } = new List<StatInfo>();
        public IList<TypeInfo> Types { get; set; } = new List<TypeInfo>();
        public int Weight { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BaseExperience { get; set; }

        public override string ToString()
        {

            string abilities = string.Join("\r\n", Abilities);
            string stats = string.Join("\r\n", Stats);
            string types = string.Join("\r\n", Types);

            return string.Concat(Name, "\r\n", abilities, "\r\n", stats, "\r\n", types, "\r\n");
        }
    }
}