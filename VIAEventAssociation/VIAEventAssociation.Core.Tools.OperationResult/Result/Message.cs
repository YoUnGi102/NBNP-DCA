namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public class Message
{
    private string? message { get; init; }
    private HTTPCodes? code { get; init; }
    
    public Message(string message)
    {
        this.message = message;
    }
    
    public Message(HTTPCodes code)
    {
        this.code = code;
    }
    
    public Message(string message, HTTPCodes code)
    {
        this.message = message;
        this.code = code;
    }
    
    public string? GetMessage()
    {
        return message;
    }
    
    public HTTPCodes? GetCode()
    {
        return code;
    }
}