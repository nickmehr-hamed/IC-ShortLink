using FluentValidation;

namespace ShortLink.Application.Links.Commands;

public class CreateLinkCommandValidator : AbstractValidator<CreateLinkCommand>
{
    public CreateLinkCommandValidator() : base()
    {
        RuleFor(current => current.Title)
            .NotEmpty().WithMessage(errorMessage: Resources.Messages.ErrorRequiredFluent)
            .MaximumLength(maximumLength: 100).WithMessage(errorMessage: Resources.Messages.ErrorMaximumLength);

        RuleFor(current => current.Url)
            .NotEmpty().WithMessage(errorMessage: Resources.Messages.ErrorRequiredFluent)
            .MaximumLength(maximumLength: 1000).WithMessage(errorMessage: Resources.Messages.ErrorMaximumLength);

        RuleFor(current => current.OwnerId)
            .NotEmpty().WithMessage(errorMessage: Resources.Messages.ErrorRequiredFluent);
    }
}
