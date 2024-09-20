using System.Net;

namespace FRESHY.Common.Domain.Common.Interfaces;

public class QueryResult<TData> : IResult
{
    public QueryResult(TData data)
    {
        Succeeded = true;
        Message = null;
        Data = data;
        StatusCode = HttpStatusCode.OK;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public QueryResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public QueryResult(HttpStatusCode statusCode, string message)
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