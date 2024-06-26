﻿namespace Rainfall.Core.Responses;

/// <summary>
/// Rainfall reading response
/// </summary>
public class RainfallReadingResponse
{
    public List<RainfallReading> Readings { get; set; }
    public RainfallReadingResponse(List<RainfallReading> readings)
    {
        Readings = readings;
    }
}

/// <summary>
/// Details of rainfall reading
/// </summary>
public class RainfallReading
{
    public DateTime DateMeasured { get; set; }

    public double AmountMeasured { get; set; }

    public RainfallReading(DateTime dateMeasured, double amountMeasured)
    {
        DateMeasured = dateMeasured;
        AmountMeasured = amountMeasured;
    }
}
