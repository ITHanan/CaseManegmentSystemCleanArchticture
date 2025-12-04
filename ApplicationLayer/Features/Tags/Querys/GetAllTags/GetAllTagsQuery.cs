using ApplicationLayer.Features.Tags.Dtos;
using DomainLayer.Common;
using MediatR;

public record GetAllTagsQuery
    : IRequest<OperationResult<IEnumerable<TagDto>>>;
