using Authentication.BusinessLogicLayer.DataTransferObjects;
using FluentValidation;

namespace Authentication.BusinessLogicLayer.Validators;

public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
{
    public UserForRegistrationDtoValidator()
    {
        RuleFor(u => u.FirstName)
            .NotNull()
            .WithMessage("Enter first name");

        RuleFor(u => u.LastName)
            .NotNull()
            .WithMessage("Enter last name");

        RuleFor(u => u.UserName)
            .NotNull()
            .WithMessage("Enter username");

        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Enter password");
    }
}