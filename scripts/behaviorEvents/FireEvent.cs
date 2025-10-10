using System.Collections.Generic;

public class FireEvent : BehaviorEvent, DebugPrint
{
  public override BehaviorEventType Type => BehaviorEventType.Fire;

  public FireParameters? fireParameters = null;

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return ["fireParameters".With(fireParameters)!];
  }
}
