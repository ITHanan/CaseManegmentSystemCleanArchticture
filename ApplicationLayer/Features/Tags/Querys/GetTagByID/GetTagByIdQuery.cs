using ApplicationLayer.Features.Tags.Dtos;
using DomainLayer.Common;
using MediatR;

public record GetTagByIdQuery(int Id)
    : IRequest<OperationResult<TagDto>>;
