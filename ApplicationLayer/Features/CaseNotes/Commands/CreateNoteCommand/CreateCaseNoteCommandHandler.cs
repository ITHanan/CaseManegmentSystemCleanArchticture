using ApplicationLayer.Common;
using ApplicationLayer.Features.CaseNotes.Commands.CreateNoteCommand;
using ApplicationLayer.Features.CaseNotes.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.CaseNotes.Commands.CreateCaseNote
{
    public class CreateCaseNoteCommandHandler
        : IRequestHandler<CreateCaseNoteCommand, OperationResult<CaseNoteDto>>
    {
        private readonly IGenericRepository<CaseNote> _noteRepo;
        private readonly IGenericRepository<Case> _caseRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreateCaseNoteCommandHandler(
            IGenericRepository<CaseNote> noteRepo,
            IGenericRepository<Case> caseRepo,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _noteRepo = noteRepo;
            _caseRepo = caseRepo;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<CaseNoteDto>> Handle(CreateCaseNoteCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == 0)
                return OperationResult<CaseNoteDto>.Failure("Unauthorized: User must be logged in.");

            // Check case exists
            var caseResult = await _caseRepo.GetByIdAsync(request.CaseId);
            if (!caseResult.IsSuccess)
                return OperationResult<CaseNoteDto>.Failure("Case not found.");

            var note = new CaseNote
            {
                CaseId = request.CaseId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = _currentUser.UserId
            };

            var createResult = await _noteRepo.AddAsync(note, cancellationToken);

            if (!createResult.IsSuccess)
                return OperationResult<CaseNoteDto>.Failure(createResult.ErrorMessage!);

            return OperationResult<CaseNoteDto>.Success(
                _mapper.Map<CaseNoteDto>(createResult.Data!)
            );
        }
    }
}
