namespace FRESHY.Common.Contract.Wrappers;

public class Response<TData>
{
    public Response(TData data)
    {
        Succeeded = true;
        Message = null;
        Data = data;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Response(string message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Succeeded = false;
        Message = message;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Response()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public TData Data { get; set; }
}