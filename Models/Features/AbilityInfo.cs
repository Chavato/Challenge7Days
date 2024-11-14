using Challenge7Days.Models.Common;

namespace Challenge7Days.Models.Features
{
    public class AbilityInfo
    {
        public GenericInfo? Ability { get; set; }
        public bool IsHidden { get; set; }
        public int Slot { get; set; }

        public override string ToString()
        {
            return $"Ability: {Ability.Name}";
        }
    }
}