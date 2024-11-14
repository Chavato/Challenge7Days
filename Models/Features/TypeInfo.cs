using Challenge7Days.Models.Common;

namespace Challenge7Days.Models.Features
{
    public class TypeInfo
    {
        public int Slot { get; set; }
        public GenericInfo? Type { get; set; }

        public override string ToString()
        {
            return $"Type: {Type.Name}";
        }
    }
}