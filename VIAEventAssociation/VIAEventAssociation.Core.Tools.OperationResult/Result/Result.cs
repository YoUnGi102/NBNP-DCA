namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public interface Result<T>
{   
    public T? GetObj();
    public Message[]? GetMessages();

    public Boolean isFailure();
}