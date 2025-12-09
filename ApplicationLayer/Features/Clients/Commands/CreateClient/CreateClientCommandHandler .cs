using ApplicationLayer.Common;
using ApplicationLayer.Features.Clients.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler
        : IRequestHandler<CreateClientCommand, OperationResult<ClientDto>>
    {
        private readonly IGenericRepository<Client> _repo;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(
            IGenericRepository<Client> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<ClientDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var entity = new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _repo.AddAsync(entity, cancellationToken);

            if (!result.IsSuccess)
                return OperationResult<ClientDto>.Failure(result.ErrorMessage!);

            return OperationResult<ClientDto>.Success(_mapper.Map<ClientDto>(result.Data!));
        }
    }
}
