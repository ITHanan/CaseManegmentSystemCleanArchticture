using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.ChangeCaseStatus.Commands
{
    public class UpdateCaseStatusCommandHandler
    : IRequestHandler<UpdateCaseStatusCommand, OperationResult<bool>>
    {
        private readonly ICaseRepository _caseRepo;
        private readonly ICurrentUserService _currentUser;

        public UpdateCaseStatusCommandHandler(
            ICaseRepository caseRepo,
            ICurrentUserService currentUser)
        {
            _caseRepo = caseRepo;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<bool>> Handle(
            UpdateCaseStatusCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == 0)
                return OperationResult<bool>.Failure("Unauthorized user.");

            var caseEntity = await _caseRepo.GetCaseWithDetailsAsync(request.CaseId);

            if (caseEntity == null)
                return OperationResult<bool>.Failure("Case not found.");

            // Authorization Rules
            if (request.Status == CaseStatus.Closed && _currentUser.Role != "Admin")
            {
                return OperationResult<bool>.Failure("Only Administrator can close cases.");
            }

            if (request.Status == CaseStatus.InProgress &&
                caseEntity.AssignedToUserId != _currentUser.UserId &&
                _currentUser.Role != "Admin")
            {
                return OperationResult<bool>.Failure("Only assigned user or admin can start working on this case.");
            }

            // Update
            caseEntity.Status = request.Status;
            caseEntity.UpdatedAt = DateTime.UtcNow;
            caseEntity.UpdatedByUserId = _currentUser.UserId;

            if (request.Status == CaseStatus.Closed)
                caseEntity.ClosedAt = DateTime.UtcNow;

            await _caseRepo.UpdateAsync(caseEntity, cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }

}

