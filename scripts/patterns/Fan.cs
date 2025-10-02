using System.Collections.Generic;
using Godot;

[Tool]
[GlobalClass]
public partial class Fan : BulletPattern
{
  [Export]
  public float angle = 30;

  [Export]
  public int count = 1;

  [Export]
  public float delayPerBullet = 0;

  public override IEnumerable<BulletSpawn> Process(
    IEnumerable<IEnumerable<BulletSpawn>> childSpawns
  )
  {
    return childSpawns.Flatten().Fan(angle, count, delayPerBullet);
  }
}
