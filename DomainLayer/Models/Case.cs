namespace DomainLayer.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public CaseStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public int? UpdatedByUserId { get; set; }
        public User? UpdatedByUser { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public int? AssignedToUserId { get; set; }
        public User? AssignedTo { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public ICollection<CaseNote> Notes { get; set; } = new List<CaseNote>();
        public ICollection<CaseTag> CaseTags { get; set; } = new List<CaseTag>();
    }

    public enum CaseStatus
    {
        Open,
        InProgress,
        Closed
    }
}