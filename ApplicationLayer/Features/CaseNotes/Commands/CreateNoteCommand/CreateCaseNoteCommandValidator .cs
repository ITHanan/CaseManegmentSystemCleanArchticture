using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Commands.CreateNoteCommand
{
    public class CreateCaseNoteCommandValidator : AbstractValidator<CreateCaseNoteCommand>
    {
        public CreateCaseNoteCommandValidator()
        {
            RuleFor(x => x.CaseId)
                .GreaterThan(0);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty.")
                .MaximumLength(2000).WithMessage("Note cannot exceed 2000 characters.");
        }
    }
}
