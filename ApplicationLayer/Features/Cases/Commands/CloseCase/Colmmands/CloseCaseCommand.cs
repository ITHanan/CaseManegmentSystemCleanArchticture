using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Cases.Commands.CloseCase.Colmmands
{
    public record CloseCaseCommand(int CaseId): IRequest<OperationResult<bool>>;
   
}
