using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class FireParameters : Resource, DebugPrint
{
  [Export]
  public string? firePattern { get; set; }

  [Export]
  public bool atPlayer { get; set; } = true;

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return ["firePatterns".With(firePattern)!, "atPlayer".With(atPlayer)];
  }
}
