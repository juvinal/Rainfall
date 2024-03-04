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
    [ProducesResponseType(typeof(GetRainfallResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    [EndpointSummary("Get rainfall readings by station Id")]
    [EndpointDescription("Retrieve the latest readings for the specified stationId")]
    [HttpGet("id/{stationId}/readings", Name = "GetAsset")]
    public async Task<ActionResult<GetRainfallResponse>> Get([FromRoute] string stationId, [FromQuery] int count = 10)
    {
        var message = new GetRainfall(stationId, count);
        var result = await _mediator.Send(message);

        return this.ToActionResult(result);
    }
}
