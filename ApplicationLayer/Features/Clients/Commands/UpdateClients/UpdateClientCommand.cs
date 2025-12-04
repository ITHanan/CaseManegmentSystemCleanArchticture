using ApplicationLayer.Features.Clients.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Clients.Commands.UpdateClient
{
    public record UpdateClientCommand(
        int Id,
        string Name,
        string Email,
        string PhoneNumber
    ) : IRequest<OperationResult<ClientDto>>;
}
