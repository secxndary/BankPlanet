using Authentication.BusinessLogicLayer.DataTransferObjects;
using FluentValidation;

namespace Authentication.BusinessLogicLayer.Validators;

public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
{
    public UserForRegistrationDtoValidator()
    {
        RuleFor(u => u.FirstName)
            .NotNull()
            .WithMessage("Введите имя")
            .MaximumLength(100)
            .WithMessage("Максимальная длина имени – 100 символов");

        RuleFor(u => u.LastName)
            .NotNull()
            .WithMessage("Введите фамилию")
            .MaximumLength(100)
            .WithMessage("Максимальная длина фамилии – 100 символов");

        RuleFor(u => u.UserName)
            .NotNull()
            .WithMessage("Введите имя пользователя")
            .MaximumLength(50)
            .WithMessage("Максимальная длина имени пользователя – 50 символов");

        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Введите пароль")
            .MaximumLength(100)
            .WithMessage("Максимальная длина пароля – 100 символов");
    }
}