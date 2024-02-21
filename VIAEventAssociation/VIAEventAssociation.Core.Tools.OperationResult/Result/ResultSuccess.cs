namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public class ResultSuccess<T> : Result<T>
{
    private string? message { get; init; }
    private HTMLCodes? code { get; init; }
    private T? obj { get; init; }

    private ResultSuccess()
    {
    }

    private ResultSuccess(T obj)
    {
        this.obj = obj;
    }

    private ResultSuccess(T obj, string message)
    {
        this.obj = obj;
        this.message = message;
    }

    private ResultSuccess(T obj, HTMLCodes code)
    {
        this.obj = obj;
        this.code = code;
    }

    public static Result<T> CreateEmptyResult()
    {
        return new ResultSuccess<T>();
    }

    public static Result<T> CreateSimpleResult(T obj)
    {
        return new ResultSuccess<T>(obj);
    }

    public static Result<T> CreateMessageResult(T obj, string message)
    {
        return new ResultSuccess<T>(obj, message);
    }

    public static Result<T> CreateHtmlResult(T obj, HTMLCodes code)
    {
        if (Enum.IsDefined(typeof(HTMLCodes), code))
        {
            return new ResultSuccess<T>(obj, code);
        }

        throw new ArgumentException("Invalid HTML code");
    }
}