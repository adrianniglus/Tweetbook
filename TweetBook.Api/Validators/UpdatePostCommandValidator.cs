using FluentValidation;
using TweetBook.Contracts.V1.Commands.Posts;

namespace TweetBook.Api.Validators
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostMergedCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
