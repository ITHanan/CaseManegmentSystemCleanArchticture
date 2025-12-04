using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Commands.DeleteNote
{
    public record DeleteCaseNoteCommand(int NoteId)
       : IRequest<OperationResult<bool>>;
}
