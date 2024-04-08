namespace VIAEventAssociation.Core.Tools.OperationResult.Result;

public class ResultHelper<T>
{
    public static Message[] CombineResultMessages(Result<T>[] results)
    {
        var messages = new List<Message>();
        foreach (var result in results)
        {
            if (result.GetMessages() != null)
            {
                messages.AddRange(result.GetMessages()!);
            }
        }
        return messages.ToArray();
    }
}