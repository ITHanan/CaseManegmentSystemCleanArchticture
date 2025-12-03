namespace DomainLayer.Models
{
    public class CaseTag
    {
        public int CaseId { get; set; }
        public Case Case { get; set; } = default!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = default!;
    }
}