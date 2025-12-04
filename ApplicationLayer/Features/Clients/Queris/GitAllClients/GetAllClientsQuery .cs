using ApplicationLayer.Features.Clients.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Clients.Queries.GetAllClients
{
    public record GetAllClientsQuery
        : IRequest<OperationResult<IEnumerable<ClientDto>>>;
}
