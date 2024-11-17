using Challenge7Days.Models.Common;

namespace Challenge7Days.Models.Features
{
    public class StatInfo
    {
        public int BaseStat { get; set; }
        public int Effort { get; set; }
        public GenericInfo? Stat { get; set; }

        public override string ToString()
        {
            return $"Stat: {Stat.Name} - {BaseStat}";
        }
    }
}