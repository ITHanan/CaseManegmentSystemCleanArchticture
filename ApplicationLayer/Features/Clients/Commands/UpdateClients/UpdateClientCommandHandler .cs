using ApplicationLayer.Common;
using ApplicationLayer.Features.Clients.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler
        : IRequestHandler<UpdateClientCommand, OperationResult<ClientDto>>
    {
        private readonly IGenericRepository<Client> _repo;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(
            IGenericRepository<Client> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<ClientDto>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);

            if (!existing.IsSuccess)
                return OperationResult<ClientDto>.Failure("Client not found");

            var entity = existing.Data!;
            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.PhoneNumber = request.PhoneNumber;

            var result = await _repo.UpdateAsync(entity, cancellationToken);

            if (!result.IsSuccess)
                return OperationResult<ClientDto>.Failure(result.ErrorMessage!);

            return OperationResult<ClientDto>.Success(_mapper.Map<ClientDto>(result.Data!));
        }
    }
}
