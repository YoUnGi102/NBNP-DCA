namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public class ResultFailure<T> : Result<T>
{
    private string? message { get; init; }
    private HTMLCodes? code { get; init; }
    private T? obj { get; init; }

    private ResultFailure()
    {
    }

    private ResultFailure(T obj)
    {
        this.obj = obj;
    }

    private ResultFailure(T obj, string message)
    {
        this.obj = obj;
        this.message = message;
    }

    private ResultFailure(T obj, HTMLCodes code)
    {
        this.obj = obj;
        this.code = code;
    }

    public static Result<T> CreateEmptyResult()
    {
        return new ResultFailure<T>();
    }

    public static Result<T> CreateSimpleResult(T obj)
    {
        return new ResultFailure<T>(obj);
    }

    public static Result<T> CreateMessageResult(T obj, string message)
    {
        return new ResultFailure<T>(obj, message);
    }

    public static Result<T> CreateHtmlResult(T obj, HTMLCodes code)
    {
        if (Enum.IsDefined(typeof(HTMLCodes), code))
        {
            return new ResultFailure<T>(obj, code);
        }

        throw new ArgumentException("Invalid HTML code");
    }
}