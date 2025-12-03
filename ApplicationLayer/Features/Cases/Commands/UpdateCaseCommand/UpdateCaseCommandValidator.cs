using FluentValidation;

namespace ApplicationLayer.Features.Cases.Commands.UpdateCase
{
    public class UpdateCaseCommandValidator : AbstractValidator<UpdateCaseCommand>
    {
        public UpdateCaseCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Invalid case ID.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid status value.");
        }
    }
}
