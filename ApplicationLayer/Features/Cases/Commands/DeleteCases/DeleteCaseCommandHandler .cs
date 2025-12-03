using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Cases.Commands.DeleteCase
{
    public class DeleteCaseCommandHandler
        : IRequestHandler<DeleteCaseCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Case> _repo;

        public DeleteCaseCommandHandler(IGenericRepository<Case> repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCaseCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteByIdAsync(request.Id);
        }
    }
}
