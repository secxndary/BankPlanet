using Cards.BusinessLogicLayer.Application.Transactions.Commands;
using Cards.BusinessLogicLayer.Application.Transactions.Queries;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using Cards.DataAccessLayer.Entities.ErrorModel;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cards.PresentationLayer.Controllers.ModelsControllers;

[ApiController]
[Route(Constants.ApiController)]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class TransactionsController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTransactionsAsync([FromQuery] TransactionParameters transactionParameters, CancellationToken cancellationToken)
    {
        var (transactions, metaData) = await sender.Send(new GetTransactionsQuery(transactionParameters), cancellationToken);

        AddPaginationHeader(metaData);

        return Ok(transactions);
    }

    [HttpGet("{id:guid}", Name = Constants.GetTransactionById)]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await sender.Send(new GetTransactionByIdQuery(id), cancellationToken);

        return Ok(transaction);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] TransactionForCreationDto transaction, CancellationToken cancellationToken)
    {
        var createdTransaction = await sender.Send(new CreateTransactionCommand(transaction), cancellationToken);

        return CreatedAtRoute(Constants.GetTransactionById, new { id = createdTransaction.Id }, createdTransaction);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTransactionAsync(Guid id, [FromBody] TransactionForUpdateDto transaction, CancellationToken cancellationToken)
    {
        var updatedTransaction = await sender.Send(new UpdateTransactionCommand(id, transaction), cancellationToken);

        return Ok(updatedTransaction);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransactionAsync(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTransactionCommand(id), cancellationToken);

        return NoContent();
    }
}