using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

[Serializable]
public class DialogActorInfo : DebugPrint
{
  [YamlMember]
  public PortraitSide? side;

  [YamlMember]
  public bool? flipped;

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(side).With(side)!, nameof(flipped).With(flipped)!];
  }
}
