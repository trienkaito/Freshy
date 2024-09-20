using System.Net;
using FRESHY.Common.Domain.Common.Interfaces;

namespace FRESHY.Common.Domain.Common.Models.Wrappers;

public class ReturnCommandResult<TData> : IResult
{
    public ReturnCommandResult(TData data)
    {
        Succeeded = true;
        Message = null;
        Data = data;
        StatusCode = HttpStatusCode.OK;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ReturnCommandResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ReturnCommandResult(HttpStatusCode statusCode, string message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Succeeded = false;
        Message = message;
        StatusCode = statusCode;
    }

    public bool Succeeded { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
    public TData Data { get; set; }
}