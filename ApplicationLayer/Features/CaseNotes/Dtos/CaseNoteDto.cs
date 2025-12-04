using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Dtos
{
    public class CaseNoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public int CaseId { get; set; }
    }
}
