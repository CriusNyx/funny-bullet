using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class SpawnOne : BulletPattern
{
  [Export]
  public Vector2 position = Vector2.Zero;

  [Export]
  public float delay = 0;

  [Export]
  public float speed = GameStats.defaultBulletSpeed;

  [Export]
  public float angle = 0f;

  [Export]
  public float lifetime = -1f;

  public override IEnumerable<BulletSpawn> Process(IEnumerable<BulletSpawn> childSpawns)
  {
    return
    [
      new BulletSpawn
      {
        position = position,
        spawnTime = delay,
        speed = speed,
        angle = angle,
        lifetime = lifetime,
      },
    ];
  }
}
