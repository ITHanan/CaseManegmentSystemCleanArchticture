using ApplicationLayer.Features.Cases.Commands.CreatCases;
using FluentValidation;

namespace ApplicationLayer.Features.Cases.Commands.CreateCase
{
    public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                .WithMessage("ClientId must be greater than 0.");

            RuleFor(x => x.AssignedToUserId)
                .GreaterThan(0)
                .When(x => x.AssignedToUserId.HasValue)
                .WithMessage("AssignedToUserId must be a valid user ID.");


            RuleFor(x => x.TagIds)
               .NotNull().WithMessage("TagIds cannot be null.");

            RuleForEach(x => x.TagIds)
                .GreaterThan(0)
                .WithMessage("Each Tag ID must be a positive number.");

            RuleFor(x => x.TagIds)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("TagIds cannot contain duplicate values.");
        }
    }
}
