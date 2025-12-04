using ApplicationLayer.Features.Tags.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

public class GetTagByIdQueryHandler
    : IRequestHandler<GetTagByIdQuery, OperationResult<TagDto>>
{
    private readonly IGenericRepository<Tag> _tagRepo;
    private readonly IMapper _mapper;

    public GetTagByIdQueryHandler(IGenericRepository<Tag> tagRepo, IMapper mapper)
    {
        _tagRepo = tagRepo;
        _mapper = mapper;
    }

    public async Task<OperationResult<TagDto>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _tagRepo.GetByIdAsync(request.Id);

        if (!result.IsSuccess)
            return OperationResult<TagDto>.Failure(result.ErrorMessage!);

        return OperationResult<TagDto>.Success(_mapper.Map<TagDto>(result.Data!));
    }
}
