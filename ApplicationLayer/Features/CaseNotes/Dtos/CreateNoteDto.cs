using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Dtos
{
    public class CreateNoteDto
    {
        public int CaseId { get; set; }
        public string Content { get; set; } = default!;
    }
}
