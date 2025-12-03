namespace DomainLayer.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<CaseTag> CaseTags { get; set; } = new List<CaseTag>();
    }
}