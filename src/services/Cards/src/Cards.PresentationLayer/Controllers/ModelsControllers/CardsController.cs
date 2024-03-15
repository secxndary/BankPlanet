using Cards.BusinessLogicLayer.Application.Cards.Commands;
using Cards.BusinessLogicLayer.Application.Cards.Queries;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
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
public class CardsController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCardsAsync([FromQuery] CardParameters cardParameters, CancellationToken cancellationToken)
    {
        var (cards, metaData) = await sender.Send(new GetCardsQuery(cardParameters), cancellationToken);

        AddPaginationHeader(metaData);

        return Ok(cards);
    }

    [HttpGet("{id:guid}", Name = Constants.GetCardById)]
    [ProducesResponseType(typeof(CardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCardByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var card = await sender.Send(new GetCardByIdQuery(id), cancellationToken);

        return Ok(card);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CardDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCardAsync([FromBody] CardForCreationDto card, CancellationToken cancellationToken)
    {
        var createdCard = await sender.Send(new CreateCardCommand(card), cancellationToken);

        return CreatedAtRoute(Constants.GetCardById, new { id = createdCard.Id }, createdCard);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCardAsync(Guid id, [FromBody] CardForUpdateDto card, CancellationToken cancellationToken)
    {
        var updatedCard = await sender.Send(new UpdateCardCommand(id, card), cancellationToken);

        return Ok(updatedCard);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(CardDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCardAsync(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteCardCommand(id), cancellationToken);

        return NoContent();
    }
}