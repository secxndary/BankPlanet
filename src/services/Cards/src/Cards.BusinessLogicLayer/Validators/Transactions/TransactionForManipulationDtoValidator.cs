using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using FluentValidation;

namespace Cards.BusinessLogicLayer.Validators.Transactions;

public class TransactionForManipulationDtoValidator : AbstractValidator<TransactionForManipulationDto>
{
    public TransactionForManipulationDtoValidator()
    {
        RuleFor(t => t.Amount)
            .NotNull()
            .GreaterThan(0);

        RuleFor(t => t.Timestamp)
            .NotNull();

        RuleFor(t => t.RecipientCardId)
            .NotNull();

        RuleFor(t => t.SenderCardId)
            .NotNull();
    }
}