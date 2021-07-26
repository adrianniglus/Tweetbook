using FluentValidation;
using TweetBook.Contracts.V1.Commands.Tags;

namespace TweetBook.Api.Validators
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagMergedCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x => x.TagName)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
