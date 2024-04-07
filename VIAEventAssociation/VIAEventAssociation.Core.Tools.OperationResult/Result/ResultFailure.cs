namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public class ResultFailure<T> : Result<T>
{
    private Message[]? messages { get; init; }
    private T? obj { get; init; }

    private ResultFailure()
    {
    }

    private ResultFailure(T obj)
    {
        this.obj = obj;
    }
    
    private ResultFailure(T obj, Message[] messages)
    {
        this.obj = obj;
        this.messages = messages;
    }

    private ResultFailure(T obj, string[] messages)
    {
        this.obj = obj;
        this.messages = new Message[messages.Length];
        for (int i = 0; i < messages.Length; i++)
        {
            this.messages[i] = new Message(messages[i]);
        }
    }

    private ResultFailure(T obj, HTTPCodes[] code)
    {
        this.obj = obj;
        messages = new Message[code.Length];
        for (int i = 0; i < code.Length; i++)
        {
            messages[i] = new Message(code[i]);
        }
    }

    private ResultFailure(T obj, HTTPCodes[] code, string[] messages)
    {
        this.obj = obj;
        this.messages = new Message[messages.Length];
        for (int i = 0; i < messages.Length; i++)
        {
            this.messages[i] = new Message(messages[i], code[i]);
        }
    }

    public static Result<T> CreateEmptyResult()
    {
        return new ResultFailure<T>();
    }

    public static Result<T> CreateSimpleResult(T obj)
    {
        return new ResultFailure<T>(obj);
    }

    public static Result<T> CreateMessageResult(T obj, string[] messages)
    {
        return new ResultFailure<T>(obj, messages);
    }
    
    public static Result<T> CreateMessageResult(T obj, Message[] messages)
    {
        return new ResultFailure<T>(obj, messages);
    }

    public static Result<T> CreateHTTPResult(T obj, HTTPCodes[] code)
    {
        return new ResultFailure<T>(obj, code);
    }

    public static Result<T> CreateResult(T obj, HTTPCodes[] code, string[] messages)
    {
        return new ResultFailure<T>(obj, code, messages);
    }

    public T? GetObj()
    {
        return obj;
    }

    public Message[]? GetMessages()
    {
        return messages;
    }

    public bool IsFailure()
    {
       return true;
    }
}