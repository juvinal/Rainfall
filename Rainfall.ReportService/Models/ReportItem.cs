using System.Text.Json.Serialization;

namespace Rainfall.ReportService.Models;

public class ReportItem
{
    [JsonPropertyName("@id")]
    public string? Id { get; set; }
    public DateTime DateTime { get; set; }
    public string? Measure { get; set; }
    public double Value { get; set; }
}
