namespace DomainLayer.Models
{
    public class CaseNote
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; } = default!;
    }
}