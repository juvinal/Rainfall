using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Extensions;
using Rainfall.Core.Requests;
using Rainfall.Core.Responses;

namespace Rainfall.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class RainfallController : ControllerBase
{
    private readonly IMediator _mediator;

    public RainfallController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <param name="stationId">The id of the reading station</param>
    /// <param name="count">The number of readings to return</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(RainfallReadingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Get rainfall readings by station Id")]
    [EndpointDescription("Retrieve the latest readings for the specified stationId")]
    [HttpGet("id/{stationId}/readings", Name = "get-rainfall")]
    public async Task<RainfallReadingResponse> Get([FromRoute] string stationId, [FromQuery] int count = 10)
    {
        var message = new GetRainfall(stationId, count);
        var result = await _mediator.Send(message);

        return result;
    }
}
