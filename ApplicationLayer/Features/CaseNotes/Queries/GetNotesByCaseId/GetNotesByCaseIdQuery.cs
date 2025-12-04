using ApplicationLayer.Features.CaseNotes.Dtos;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Queries.GetNotesByCaseId
{
    public record GetNotesByCaseIdQuery(int CaseId)
        : IRequest<OperationResult<List<CaseNoteDto>>>;
}
