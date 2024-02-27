using Authentication.BusinessLogicLayer.DataTransferObjects;
using FluentValidation;

namespace Authentication.BusinessLogicLayer.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotNull()
            .WithMessage("Enter username");

        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Enter password");
    }
}