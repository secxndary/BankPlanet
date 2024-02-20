using Authentication.BusinessLogicLayer.DataTransferObjects;
using FluentValidation;

namespace Authentication.BusinessLogicLayer.Validators;

public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
{
    public UserForRegistrationDtoValidator()
    {
        RuleFor(u => u.FirstName)
            .NotNull()
            .WithMessage("Введите имя");

        RuleFor(u => u.LastName)
            .NotNull()
            .WithMessage("Введите фамилию");

        RuleFor(u => u.UserName)
            .NotNull()
            .WithMessage("Введите имя пользователя");

        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Введите пароль");
    }
}