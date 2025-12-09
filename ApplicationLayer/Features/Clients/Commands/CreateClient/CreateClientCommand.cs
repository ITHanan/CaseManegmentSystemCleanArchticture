using ApplicationLayer.Common;
using ApplicationLayer.Features.Clients.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Clients.Commands.CreateClient
{
    public record CreateClientCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber
    ) : IRequest<OperationResult<ClientDto>>;
}
