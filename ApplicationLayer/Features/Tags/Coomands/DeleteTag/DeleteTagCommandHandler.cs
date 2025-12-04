using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

public class DeleteTagCommandHandler
    : IRequestHandler<DeleteTagCommand, OperationResult<bool>>
{
    private readonly IGenericRepository<Tag> _tagRepo;

    public DeleteTagCommandHandler(IGenericRepository<Tag> tagRepo)
    {
        _tagRepo = tagRepo;
    }

    public async Task<OperationResult<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        return await _tagRepo.DeleteByIdAsync(request.Id);
    }
}
