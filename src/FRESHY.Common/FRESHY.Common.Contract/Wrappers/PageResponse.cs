namespace FRESHY.Common.Contract.Wrappers;

public class PageResponse<TData> : Response<TData>
{
    public PageResponse(int totalpage, int pageNumber, int pageSize, string message)
    {
        TotalPage = totalpage;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Message = message;
        Succeeded = false;
    }

    public PageResponse(TData data, int totalpage, int pageNumber, int pageSize)
    {
        TotalPage = totalpage;

        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Succeeded = true;
        Message = null;
    }

    public PageResponse()
    {
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
}