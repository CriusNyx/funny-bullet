using System.Collections.Generic;
using Godot;

[Tool]
[GlobalClass]
public partial class Sequence : BulletPattern
{
  [Export]
  public float delay = 0f;

  public override IEnumerable<BulletSpawn> Process(
    IEnumerable<IEnumerable<BulletSpawn>> childSpawns
  )
  {
    return BulletSpawn.Sequence(delay, childSpawns);
  }
}
