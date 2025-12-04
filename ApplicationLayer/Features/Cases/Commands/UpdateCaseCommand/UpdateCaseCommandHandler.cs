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
        private readonly IGenericRepository<Case> _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateCaseCommandHandler(
            IGenericRepository<Case> repo,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<CaseDto>> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == 0)
                return OperationResult<CaseDto>.Failure("Unauthorized: User not authenticated.");

            var existing = await _repo.GetByIdAsync(request.Id);
            if (!existing.IsSuccess)
                return OperationResult<CaseDto>.Failure("Case not found");

            var entity = existing.Data!;

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Status = request.Status;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedByUserId = _currentUser.UserId;

            entity.CaseTags.Clear();

            foreach (var tagId in request.TagIds)
            {
                entity.CaseTags.Add(new CaseTag { TagId = tagId });
            }

            var result = await _repo.UpdateAsync(entity, cancellationToken);

            if (!result.IsSuccess)
                return OperationResult<CaseDto>.Failure(result.ErrorMessage!);

            return OperationResult<CaseDto>.Success(_mapper.Map<CaseDto>(result.Data!));
        }
    }
}
