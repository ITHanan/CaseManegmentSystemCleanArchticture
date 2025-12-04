using ApplicationLayer.Features.Tags.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

public class CreateTagCommandHandler
    : IRequestHandler<CreateTagCommand, OperationResult<TagDto>>
{
    private readonly IGenericRepository<Tag> _tagRepo;
    private readonly IMapper _mapper;

    public CreateTagCommandHandler(IGenericRepository<Tag> tagRepo, IMapper mapper)
    {
        _tagRepo = tagRepo;
        _mapper = mapper;
    }

    public async Task<OperationResult<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag { Name = request.Name };

        var result = await _tagRepo.AddAsync(tag, cancellationToken);

        if (!result.IsSuccess)
            return OperationResult<TagDto>.Failure(result.ErrorMessage!);

        return OperationResult<TagDto>.Success(_mapper.Map<TagDto>(result.Data!));
    }
}
