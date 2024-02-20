using Authentication.BusinessLogicLayer.DataTransferObjects;
using FluentValidation;

namespace Authentication.BusinessLogicLayer.Validators;

public class UserForAuthenticationDtoValidator : AbstractValidator<UserForRegistrationDto>
{
    public UserForAuthenticationDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotNull()
            .WithMessage("Введите имя пользователя");

        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Введите пароль");
    }
}