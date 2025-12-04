using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Clients.Commands.DeleteClient
{
    public record DeleteClientCommand(int Id)
        : IRequest<OperationResult<bool>>;
}
