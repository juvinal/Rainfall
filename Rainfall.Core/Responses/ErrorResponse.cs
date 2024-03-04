namespace Rainfall.Core.Responses;


/// <summary>
/// An error object returned for failed requests
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Details of the error message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Array containing error details.
    /// </summary>
    public List<ErrorDetail> Detail { get; set; } = new List<ErrorDetail>();
}

/// <summary>
/// Details of invalid request property
/// </summary>
public class ErrorDetail
{
    public ErrorDetail(string propertyName, string message)
    {
        PropertyName = propertyName;
        Message = message;
    }

    /// <summary>
    /// Name of the invalid request property.
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// Details of the error message.
    /// </summary>
    public string Message { get; set; }
}
