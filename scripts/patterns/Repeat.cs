using System.Collections.Generic;
using Godot;

[Tool]
[GlobalClass]
public partial class Repeat : BulletPattern
{
  [Export]
  public int repeatCount = 1;

  [Export]
  public float delayPerVolley = 0;

  public override IEnumerable<BulletSpawn> Process(
    IEnumerable<IEnumerable<BulletSpawn>> childSpawns
  )
  {
    return childSpawns.Flatten().Repeat(repeatCount, delayPerVolley);
  }
}
