using System.Text.Json.Serialization;

namespace Rainfall.ReportService.Models;

public class RainfallReport
{
    [JsonPropertyName("@context")]
    public string Context { get; set; }
    public Meta Meta { get; set; }
    public List<ReportItem> Items { get; set; }
}
