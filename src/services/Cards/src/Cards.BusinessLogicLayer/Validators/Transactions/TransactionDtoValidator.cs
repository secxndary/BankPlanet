using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using FluentValidation;

namespace Cards.BusinessLogicLayer.Validators.Transactions;

public class TransactionDtoValidator : AbstractValidator<TransactionDto>
{
    public TransactionDtoValidator()
    {
        RuleFor(t => t.Amount)
            .NotNull()
            .GreaterThan(0);

        RuleFor(t => t.Timestamp)
            .NotNull();

        RuleFor(t => t.SenderCardId)
            .NotNull();

        RuleFor(t => t.RecipientCardId)
            .NotNull();
    }
}