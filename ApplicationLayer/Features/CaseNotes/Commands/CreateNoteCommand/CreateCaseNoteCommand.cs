using ApplicationLayer.Features.CaseNotes.Dtos;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Commands.CreateNoteCommand
{
    public record CreateCaseNoteCommand(int CaseId, string Content)
          : IRequest<OperationResult<CaseNoteDto>>;
}
