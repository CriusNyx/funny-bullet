using System.Collections.Generic;
using System.Linq;
using Godot;

[Tool]
[GlobalClass]
public partial class InterleavedFan : BulletPattern
{
  [Export]
  public int repeatCount = 1;

  [Export]
  public float outerAngle = 30;

  [Export]
  public int count = 5;

  [Export]
  public float delayPerVolley = 0;

  [Export]
  public bool withLastFan = false;

  public override IEnumerable<BulletSpawn> Process(
    IEnumerable<IEnumerable<BulletSpawn>> childSpawns
  )
  {
    return childSpawns
      .Flatten()
      .SelectMany(x =>
        x.InterleavedFan(repeatCount, outerAngle, count, delayPerVolley, withLastFan)
      );
  }
}
