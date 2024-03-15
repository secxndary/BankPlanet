using Cards.BusinessLogicLayer.Application.CardTypes.Commands;
using Cards.BusinessLogicLayer.Application.CardTypes.Queries;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using Cards.DataAccessLayer.Entities.ErrorModel;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cards.PresentationLayer.Controllers.ModelsControllers;

[ApiController]
[Route(Constants.CardTypesRoute)]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class CardTypesController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CardTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCardTypesAsync([FromQuery] CardTypeParameters cardTypeParameters, CancellationToken cancellationToken)
    {
        var (cardTypes, metaData) = await sender.Send(new GetCardTypesQuery(cardTypeParameters), cancellationToken);

        AddPaginationHeader(metaData);

        return Ok(cardTypes);
    }

    [HttpGet("{id:guid}", Name = Constants.GetCardTypeById)]
    [ProducesResponseType(typeof(CardTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCardTypeByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cardType = await sender.Send(new GetCardTypeByIdQuery(id), cancellationToken);

        return Ok(cardType);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CardTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCardTypeAsync([FromBody] CardTypeForCreationDto cardType, CancellationToken cancellationToken)
    {
        var createdCardType = await sender.Send(new CreateCardTypeCommand(cardType), cancellationToken);

        return CreatedAtRoute(Constants.GetCardTypeById, new { id = createdCardType.Id }, createdCardType);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CardTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCardTypeAsync(Guid id, [FromBody] CardTypeForUpdateDto cardType, CancellationToken cancellationToken)
    {
        var updatedCardType = await sender.Send(new UpdateCardTypeCommand(id, cardType), cancellationToken);

        return Ok(updatedCardType);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(CardTypeDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCardTypeAsync(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteCardTypeCommand(id), cancellationToken);

        return NoContent();
    }
}