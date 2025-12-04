using ApplicationLayer.Features.Tags.Dtos;
using DomainLayer.Common;
using MediatR;

public record UpdateTagCommand(int Id, string Name)
    : IRequest<OperationResult<TagDto>>;
