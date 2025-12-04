using ApplicationLayer.Features.Tags.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

public class GetAllTagsQueryHandler
    : IRequestHandler<GetAllTagsQuery, OperationResult<IEnumerable<TagDto>>>
{
    private readonly IGenericRepository<Tag> _tagRepo;
    private readonly IMapper _mapper;

    public GetAllTagsQueryHandler(IGenericRepository<Tag> tagRepo, IMapper mapper)
    {
        _tagRepo = tagRepo;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<TagDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var result = await _tagRepo.GetAllAsync();

        if (!result.IsSuccess)
            return OperationResult<IEnumerable<TagDto>>.Failure(result.ErrorMessage!);

        var dtoList = _mapper.Map<IEnumerable<TagDto>>(result.Data!);

        return OperationResult<IEnumerable<TagDto>>.Success(dtoList);
    }
}

