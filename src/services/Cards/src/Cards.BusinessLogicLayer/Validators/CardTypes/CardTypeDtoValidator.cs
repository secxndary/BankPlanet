using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using FluentValidation;

namespace Cards.BusinessLogicLayer.Validators.CardTypes;

public class CardTypeDtoValidator : AbstractValidator<CardTypeDto>
{
    public CardTypeDtoValidator()
    {
        RuleFor(cardType => cardType.Name)
            .NotNull()
            .Length(1, 300);

        RuleFor(cardType => cardType.Description)
            .Length(0, 3000);
    }
}