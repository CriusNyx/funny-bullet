using System.Collections.Generic;
using Godot;

public partial class AI : Node3D
{
  [Export]
  public PackedScene bulletPrefab;

  static IEnumerable<BulletSpawn> Pattern1 = new BulletSpawn { lifetime = 10, speed = 10f }
    .Fan(10, 3, 0.6f)
    .Fan(30, 5, 0.4f / 5);

  static IEnumerable<BulletSpawn> Pattern2 = new BulletSpawn { lifetime = 10, speed = 10f }
    .Fan(45, 5, 0)
    .Repeat(3, 0.2f);

  static IEnumerable<BulletSpawn> Pattern3 = Pattern1.ThenSpawn(Pattern2, 1f);

  static IEnumerable<BulletSpawn> Pattern4 = new BulletSpawn
  {
    lifetime = 10,
    speed = 10,
  }.InterleavedFan(5, 75, 7, 0.4f, true);

  static IEnumerable<BulletSpawn> Pattern5 = Pattern3.ThenSpawn(Pattern4, 1f);

  public override void _Ready()
  {
    base._Ready();
    BulletSpawner.Create(
      Pattern5,
      new SpawnParameters { angle = 270, prefab = bulletPrefab },
      this
    );
  }
}
