using System.Collections.Generic;

public class DialogSelector : DialogContent, DebugPrint
{
  public string message;
  public string[] options;

  public DialogSelector(string message, string[] options)
  {
    this.message = message;
    this.options = options;
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(message).With(message), nameof(options).With(options)];
  }
}
