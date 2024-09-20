using System.Net;

namespace FRESHY.Common.Domain.Common.Models.Wrappers;

public class CommandResult
{
    public CommandResult()
    {
        Succeeded = true;
        Message = null;
        StatusCode = HttpStatusCode.OK;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CommandResult(HttpStatusCode statusCode, string? message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Succeeded = false;
        Message = message;
        StatusCode = statusCode;
    }

    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}