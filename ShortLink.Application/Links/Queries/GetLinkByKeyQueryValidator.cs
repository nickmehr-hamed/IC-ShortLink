using FluentValidation;

namespace ShortLink.Application.Links.Queries;

public class GetLinkByKeyQueryValidator : AbstractValidator<GetLinkByKeyQuery>
{
    public GetLinkByKeyQueryValidator() : base() => RuleFor(current => current.Key)
        .NotEmpty().WithMessage(Resources.Messages.ErrorRequiredFluent)
        .Length(10).WithMessage(Resources.Messages.ErrorLengthFluent);
}
