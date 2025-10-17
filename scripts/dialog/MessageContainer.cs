using System.Collections.Generic;

public class MessageContainer : IDialogMessage, DialogContent, DebugPrint
{
  public MessageContainer(string message)
  {
    Message = message;
  }

  public string Message { get; set; }

  public static MessageContainer Auto(string message)
  {
    return new MessageContainer($"[p] {message} [h] [][]");
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(Message).With(Message)];
  }
}
