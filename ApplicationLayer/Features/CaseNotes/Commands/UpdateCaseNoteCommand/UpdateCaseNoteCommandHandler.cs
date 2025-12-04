using ApplicationLayer.Features.CaseNotes.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.CaseNotes.Commands.UpdateCaseNoteCommand
{
    public class UpdateCaseNoteCommandHandler
        : IRequestHandler<UpdateCaseNoteCommand, OperationResult<CaseNoteDto>>
    {
        private readonly IGenericRepository<CaseNote> _noteRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public UpdateCaseNoteCommandHandler(
            IGenericRepository<CaseNote> noteRepo,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _noteRepo = noteRepo;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<CaseNoteDto>> Handle(UpdateCaseNoteCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == 0)
                return OperationResult<CaseNoteDto>.Failure("Unauthorized");

            // Get existing note
            var noteResult = await _noteRepo.GetByIdAsync(request.NoteId);
            if (!noteResult.IsSuccess)
                return OperationResult<CaseNoteDto>.Failure("Note not found");

            var note = noteResult.Data!;

            // Prevent editing other users' notes 
            if (note.CreatedByUserId != _currentUser.UserId)
                return OperationResult<CaseNoteDto>.Failure("You cannot edit a note you did not create.");

            // Update fields
            note.Content = request.Content;
            note.CreatedAt = DateTime.UtcNow; // OR UpdatedAt if you prefer

            var updateResult = await _noteRepo.UpdateAsync(note, cancellationToken);

            if (!updateResult.IsSuccess)
                return OperationResult<CaseNoteDto>.Failure(updateResult.ErrorMessage!);

            return OperationResult<CaseNoteDto>.Success(_mapper.Map<CaseNoteDto>(updateResult.Data!));
        }
    }
}
