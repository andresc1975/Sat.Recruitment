using Sat.Recruitment.Api.Domain;
using FluentValidation;

namespace Sat.Recruitment.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).
                NotEmpty().WithMessage("The name is required");
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("The email is required")
                .EmailAddress().WithMessage("The email format is invalid");
            RuleFor(user => user.Address)
                .NotEmpty().WithMessage("The address is required");
            RuleFor(user => user.Phone)
                .NotEmpty().WithMessage("The phone is required");
            RuleFor(user => user.Money)
                .NotEmpty().WithMessage("Money is required")
                .GreaterThanOrEqualTo(0).WithMessage("Money should be >= 0");
            RuleFor(user => user.UserType)
                .NotNull().WithMessage("Invalid User Type");
        }

    }
}
