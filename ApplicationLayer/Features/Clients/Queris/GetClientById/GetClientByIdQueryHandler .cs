using ApplicationLayer.Common;
using ApplicationLayer.Features.Clients.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler
        : IRequestHandler<GetClientByIdQuery, OperationResult<ClientDto>>
    {
        private readonly IGenericRepository<Client> _repo;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(
            IGenericRepository<Client> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetByIdAsync(request.Id);

            if (!result.IsSuccess)
                return OperationResult<ClientDto>.Failure("Client not found");

            return OperationResult<ClientDto>.Success(_mapper.Map<ClientDto>(result.Data!));
        }
    }
}
