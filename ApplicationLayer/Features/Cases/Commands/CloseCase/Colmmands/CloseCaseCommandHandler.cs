using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Cases.Commands.CloseCase.Colmmands
{
    public class CloseCaseCommandHandler
        : IRequestHandler<CloseCaseCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Case> _caseRepository;

        public CloseCaseCommandHandler(IGenericRepository<Case> CaseRepository)
        {
            _caseRepository = CaseRepository;
        }
        public async Task<OperationResult<bool>> Handle(CloseCaseCommand request, CancellationToken cancellationToken)
        {
            var result = await _caseRepository.GetByIdAsync(request.CaseId);

            if (!result.IsSuccess)
                return OperationResult<bool>.Failure("Case not found.");

            var caseToClose = result.Data!;

            caseToClose.Status = CaseStatus.Closed;
            caseToClose.ClosedAt = DateTime.UtcNow;
            var updateResult = await _caseRepository.UpdateAsync(caseToClose, cancellationToken);

             return OperationResult<bool>.Success(updateResult.IsSuccess);
        }
    }
}
