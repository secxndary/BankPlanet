using System.Text.Json;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Cards.PresentationLayer.Controllers;

public class CustomControllerBase : ControllerBase
{
    public void AddPaginationHeader(MetaData metaData)
    {
        Response.Headers.Append(Constants.PaginationHeader, JsonSerializer.Serialize(metaData));
    }
}