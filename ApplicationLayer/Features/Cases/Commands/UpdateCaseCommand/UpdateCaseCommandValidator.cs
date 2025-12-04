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

            // ✅ Validate TagIds
            RuleFor(x => x.TagIds)
                .NotNull().WithMessage("TagIds list cannot be null.");

            RuleForEach(x => x.TagIds)
                .GreaterThan(0)
                .WithMessage("Tag ID must be a positive number.");

            RuleFor(x => x.TagIds)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("TagIds cannot contain duplicate values.");
        }
    }
}
