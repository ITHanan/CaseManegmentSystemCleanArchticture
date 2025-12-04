using ApplicationLayer.Features.CaseNotes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Cases.Dtos
{
    public  class CaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = default!;
        public int? AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; }
        public string Status { get; set; } = default!;
        public List<string> Tags { get; set; } = new();
        public List<CaseNoteDto> Notes { get; set; } = new();

    }
}
