using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Commands.DeleteNote
{
    public class DeleteCaseNoteCommandHandler
     : IRequestHandler<DeleteCaseNoteCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<CaseNote> _noteRepo;
        private readonly ICurrentUserService _currentUser;

        public DeleteCaseNoteCommandHandler(
            IGenericRepository<CaseNote> noteRepo,
            ICurrentUserService currentUser)
        {
            _noteRepo = noteRepo;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCaseNoteCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == 0)
                return OperationResult<bool>.Failure("Unauthorized");

            var noteResult = await _noteRepo.GetByIdAsync(request.NoteId);
            if (!noteResult.IsSuccess)
                return OperationResult<bool>.Failure("Note not found");

            var note = noteResult.Data!;

            // Optional: restrict delete to creator
            if (note.CreatedByUserId != _currentUser.UserId)
                return OperationResult<bool>.Failure("Only the creator can delete this note.");

            var deleteResult = await _noteRepo.DeleteByIdAsync(request.NoteId);

            if (!deleteResult.IsSuccess)
                return OperationResult<bool>.Failure(deleteResult.ErrorMessage!);

            return OperationResult<bool>.Success(true);
        }
    }
