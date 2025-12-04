using ApplicationLayer.Common;
using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Cases.Commands.UpdateCase
{
    public class UpdateCaseCommandHandler
        : IRequestHandler<UpdateCaseCommand, OperationResult<CaseDto>>
    {
        private readonly ICaseRepository _caseRepo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateCaseCommandHandler(
            ICaseRepository caseRepo,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _caseRepo = caseRepo;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<CaseDto>> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == 0)
                return OperationResult<CaseDto>.Failure("Unauthorized: User not authenticated.");

            //  Load case WITH all details (tags, users, notes…)
            var entity = await _caseRepo.GetCaseWithDetailsAsync(request.Id);

            if (entity == null)
                return OperationResult<CaseDto>.Failure("Case not found.");

            // Update fields
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Status = request.Status;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedByUserId = _currentUser.UserId;

            // Update Tags
            entity.CaseTags.Clear();

            foreach (var tagId in request.TagIds)
            {
                entity.CaseTags.Add(new CaseTag { TagId = tagId, CaseId = entity.Id });
            }

            // Save changes
            var result = await _caseRepo.UpdateAsync(entity, cancellationToken);

            if (!result.IsSuccess)
                return OperationResult<CaseDto>.Failure(result.ErrorMessage!);

            var dto = _mapper.Map<CaseDto>(entity);
            return OperationResult<CaseDto>.Success(dto);
        }
    }
}
