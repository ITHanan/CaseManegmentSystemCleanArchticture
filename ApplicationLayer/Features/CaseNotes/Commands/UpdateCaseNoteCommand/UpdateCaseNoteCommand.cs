using ApplicationLayer.Features.CaseNotes.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.CaseNotes.Commands.UpdateCaseNoteCommand
{
    public record UpdateCaseNoteCommand(int NoteId, string Content)
     : IRequest<OperationResult<CaseNoteDto>>;

}
