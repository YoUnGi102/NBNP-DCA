namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public class ResultSuccess<T> : Result<T>
{
    private Message[]? messages { get; init; }
    private T? obj { get; init; }

    private ResultSuccess()
    {
    }

    private ResultSuccess(T obj)
    {
        this.obj = obj;
    }

    private ResultSuccess(T obj, string[] messages)
    {
        this.obj = obj;
        this.messages = new Message[messages.Length];
        for (int i = 0; i < messages.Length; i++)
        {
            this.messages[i] = new Message(messages[i]);
        }
    }

    private ResultSuccess(T obj, HTTPCodes[] codes)
    {
        this.obj = obj;
        messages = new Message[codes.Length];
        for (int i = 0; i < codes.Length; i++)
        {
            messages[i] = new Message(codes[i]);
        }
    }

    private ResultSuccess(T obj, HTTPCodes[] codes, string[] messages)
    {
        this.obj = obj;
        this.messages = new Message[messages.Length];
        for (int i = 0; i < messages.Length; i++)
        {
            this.messages[i] = new Message(messages[i], codes[i]);
        }
    }

    public static Result<T> CreateEmptyResult()
    {
        return new ResultSuccess<T>();
    }

    public static Result<T> CreateSimpleResult(T obj)
    {
        return new ResultSuccess<T>(obj);
    }

    public static Result<T> CreateMessageResult(T obj, string[] messages)
    {
        return new ResultSuccess<T>(obj, messages);
    }

    public static Result<T> CreateHTTPResult(T obj, HTTPCodes[] codes)
    {
        return new ResultSuccess<T>(obj, codes);
    }

    public static Result<T> CreateResult(T obj, HTTPCodes[] codes, string[] messages)
    {
        return new ResultSuccess<T>(obj, codes, messages);
    }

    public T? GetObj()
    {
        return obj;
    }

    public Message[]? GetMessages()
    {
        return messages;
    }
    
    public bool isFailure()
    {
        return false;
    }
}