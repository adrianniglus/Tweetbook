using FluentValidation;
using TweetBook.Contracts.V1.Commands.Tags;

namespace TweetBook.Api.Validators
{
    public class UpdateTagCommandValidator : AbstractValidator<CreateTagMergedCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(x => x.TagName)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
