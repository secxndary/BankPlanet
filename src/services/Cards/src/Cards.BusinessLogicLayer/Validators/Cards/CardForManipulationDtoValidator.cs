using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
using FluentValidation;

namespace Cards.BusinessLogicLayer.Validators.Cards;

public class CardForManipulationDtoValidator : AbstractValidator<CardForManipulationDto>
{
    public CardForManipulationDtoValidator()
    {
        RuleFor(c => c.Number)
            .NotNull()
            .Length(12, 20)
            .CreditCard();

        RuleFor(c => c.Balance)
            .NotNull();

        RuleFor(c => c.StartDate)
            .NotNull();

        RuleFor(c => c.ExpiryDate)
            .NotNull()
            .GreaterThan(c => c.StartDate);

        RuleFor(c => c.CardTypeId)
            .NotNull();

        RuleFor(c => c.UserId)
            .NotNull();
    }
}