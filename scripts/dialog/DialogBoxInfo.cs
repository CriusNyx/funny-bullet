using System.Collections.Generic;

public class DialogBoxInfo : DebugPrint
{
  public string actorName;
  public DialogContent[] lines;

  public DialogBoxInfo(string actorName, DialogContent[] lines)
  {
    this.actorName = actorName;
    this.lines = lines;
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(actorName).With(actorName), nameof(lines).With(lines)];
  }
}
