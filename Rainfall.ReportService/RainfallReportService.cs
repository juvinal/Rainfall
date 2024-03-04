using Microsoft.Extensions.Logging;
using Rainfall.ReportService.Models;
using System.Text.Json;

namespace Rainfall.ReportService;

public interface IRainfallReportService
{
    Task<RainfallReport> GetRainfallReadingsByStationAsync(string stationId, int limit, CancellationToken cancellationToken);
}

public class RainfallReportService : IRainfallReportService
{
    private readonly ILogger<RainfallReportService> _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public RainfallReportService(ILogger<RainfallReportService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<RainfallReport> GetRainfallReadingsByStationAsync(string stationId, int limit, CancellationToken cancellationToken)
    {
        var path = $"flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={limit}";

        var response = await _httpClient.GetAsync(path, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if(!response.IsSuccessStatusCode)
        {
            _logger.LogError($"An error occured while calling Rainfall API StatusCode:{response.StatusCode}, ReasonPhrase:{response.ReasonPhrase}, Content:{responseContent}");

            throw new Exception($"Error while calling Rainfall API StatusCode:{response.StatusCode}, ReasonPhrase:{response.ReasonPhrase}, Content:{responseContent}");
        }

        var result = JsonSerializer.Deserialize<RainfallReport>(responseContent, _jsonOptions);

        return result;
    }
}
