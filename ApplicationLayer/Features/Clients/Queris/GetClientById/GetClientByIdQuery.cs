using ApplicationLayer.Features.Clients.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Clients.Queries.GetClientById
{
    public record GetClientByIdQuery(int Id)
        : IRequest<OperationResult<ClientDto>>;
}
