using System.ComponentModel.DataAnnotations;

namespace Authentication.BusinessLogicLayer.DataTransferObjects;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Введите имя пользователя")]
    public string? UserName { get; init; }

    [Required(ErrorMessage = "Введите пароль")]
    public string? Password { get; init; }
}
