namespace Challenge7Days.Models.Common
{
    public class GenericInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}