using ApplicationLayer.Common;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Commands.DeleteCase
{
    public record DeleteCaseCommand(int Id)
        : IRequest<OperationResult<bool>>;
}
