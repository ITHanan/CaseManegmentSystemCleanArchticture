using DomainLayer.Common;
using MediatR;

public record DeleteTagCommand(int Id)
    : IRequest<OperationResult<bool>>;
