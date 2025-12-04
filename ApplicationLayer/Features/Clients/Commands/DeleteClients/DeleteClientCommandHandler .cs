using ApplicationLayer.Common;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommandHandler
        : IRequestHandler<DeleteClientCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Client> _repo;

        public DeleteClientCommandHandler(IGenericRepository<Client> repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteByIdAsync(request.Id);
        }
    }
}
