using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.ChangeCaseStatus.Commands
{
    public record UpdateCaseStatusCommand(int CaseId, CaseStatus Status)
     : IRequest<OperationResult<bool>>;

}
