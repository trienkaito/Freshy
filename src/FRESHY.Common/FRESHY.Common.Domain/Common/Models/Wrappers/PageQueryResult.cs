using System.Net;

namespace FRESHY.Common.Domain.Common.Interfaces;

public class PageQueryResult<TData> : QueryResult<TData>, IResult
{
    public PageQueryResult(int pageNumber, int pageSize, HttpStatusCode statusCode, string message)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Message = message;
        Succeeded = false;
        StatusCode = statusCode;
    }

    public PageQueryResult(TData data, int pageNumber, int pageSize, int totalPage)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Succeeded = true;
        Message = null;
        TotalPage = totalPage;
        StatusCode = HttpStatusCode.OK;
    }

    public PageQueryResult(TData data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Succeeded = true;
        Message = null;
    }

    public PageQueryResult()
    {
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
}