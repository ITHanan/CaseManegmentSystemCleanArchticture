using ApplicationLayer.Features.Clients.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler
        : IRequestHandler<GetAllClientsQuery, OperationResult<IEnumerable<ClientDto>>>
    {
        private readonly IGenericRepository<Client> _repo;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(
            IGenericRepository<Client> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync();

            if (!result.IsSuccess)
                return OperationResult<IEnumerable<ClientDto>>.Failure(result.ErrorMessage!);

            return OperationResult<IEnumerable<ClientDto>>.Success(_mapper.Map<IEnumerable<ClientDto>>(result.Data!));
        }
    }
}
