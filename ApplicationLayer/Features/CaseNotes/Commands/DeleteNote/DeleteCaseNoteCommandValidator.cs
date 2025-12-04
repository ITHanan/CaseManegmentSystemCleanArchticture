using FluentValidation;

namespace ApplicationLayer.Features.CaseNotes.Commands.DeleteNote
{
    public class DeleteCaseNoteCommandValidator : AbstractValidator<DeleteCaseNoteCommand>
    {
        public DeleteCaseNoteCommandValidator()
        {

            RuleFor(x => x.NoteId)
                .GreaterThan(0)
                .WithMessage("Invalid note ID.");
        }
    }
}
