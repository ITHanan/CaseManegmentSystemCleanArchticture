using ApplicationLayer.Features.Tags.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

public class UpdateTagCommandHandler
    : IRequestHandler<UpdateTagCommand, OperationResult<TagDto>>
{
    private readonly IGenericRepository<Tag> _tagRepo;
    private readonly IMapper _mapper;

    public UpdateTagCommandHandler(IGenericRepository<Tag> tagRepo, IMapper mapper)
    {
        _tagRepo = tagRepo;
        _mapper = mapper;
    }

    public async Task<OperationResult<TagDto>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var existing = await _tagRepo.GetByIdAsync(request.Id);

        if (!existing.IsSuccess)
            return OperationResult<TagDto>.Failure("Tag not found");

        existing.Data!.Name = request.Name;

        var updated = await _tagRepo.UpdateAsync(existing.Data, cancellationToken);

        if (!updated.IsSuccess)
            return OperationResult<TagDto>.Failure(updated.ErrorMessage!);

        return OperationResult<TagDto>.Success(_mapper.Map<TagDto>(updated.Data!));
    }
}
