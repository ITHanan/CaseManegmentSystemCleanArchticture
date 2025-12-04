using ApplicationLayer.Features.Tags.Dtos;
using DomainLayer.Common;
using MediatR;

public record CreateTagCommand(string Name)
    : IRequest<OperationResult<TagDto>>;
