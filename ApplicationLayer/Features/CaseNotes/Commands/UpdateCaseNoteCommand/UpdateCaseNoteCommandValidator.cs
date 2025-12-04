using FluentValidation;

namespace ApplicationLayer.Features.CaseNotes.Commands.UpdateCaseNoteCommand
{
    public class UpdateCaseNoteCommandValidator : AbstractValidator<UpdateCaseNoteCommand>
    {
        public UpdateCaseNoteCommandValidator()
        {
            RuleFor(x => x.NoteId)
                .GreaterThan(0)
                .WithMessage("Invalid note ID.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");
        }
    }
}
