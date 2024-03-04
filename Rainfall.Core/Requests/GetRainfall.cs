using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Rainfall.Core.Responses;
using Rainfall.ReportService;

namespace Rainfall.Core.Requests;

public class GetRainfall : IRequest<Result<GetRainfallResponse>>
{
    public GetRainfall()
    {
    }

    public GetRainfall(string stationId, int count)
    {
        this.StationId = stationId;
        this.Count = count;
    }

    public string StationId { get; set; }
    public int Count { get; set; }
}

public class GetRainfallHandler : IRequestHandler<GetRainfall, Result<GetRainfallResponse>>
{
    private readonly IRainfallReportService _service;
    public GetRainfallHandler(IRainfallReportService service)
    {
        _service = service;
    }

    public async Task<Result<GetRainfallResponse>> Handle(GetRainfall request, CancellationToken cancellationToken)
    {
        var validator = new GetRainfallValidator();
        var validations = await validator.ValidateAsync(request, cancellationToken);

        if (!validations.IsValid)
            return Result.Invalid(validations.AsErrors());

        var readings = await _service.GetRainfallReadingsByStationAsync(request.StationId, request.Count, cancellationToken);

        if (readings == null || readings.Items.Count == 0)
            return Result.NotFound(new string[] { "No readings found for the specified stationId"});

        var mappedResult = readings.Items.Select(item => new RainfallReading(item.DateTime, item.Value)).ToList();

        return Result.Success(new GetRainfallResponse(mappedResult));
    }
}
