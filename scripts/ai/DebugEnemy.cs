using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class DebugEnemy : Character
{
  [Export]
  public double lifetime;
  double timeAlive;

  IEnumerable<BulletSpawn> spawn = new BulletSpawn
  {
    angle = 0,
    lifetime = 10,
    position = Vector2.Zero,
    spawnTime = 0,
    speed = 10,
  }.InterleavedFan(5, 45, 7, 0.25f, true);

  public override void _Process(double delta)
  {
    if (timeAlive > lifetime)
    {
      Kill();
    }
    timeAlive += delta;
    base._Process(delta);
  }

  public void ShootAtPlayer()
  {
    new BulletSpawner()
      .WithSpawn(
        spawn,
        new SpawnParameters()
        {
          prefab = GD.Load<PackedScene>("res://resources/prefabs/bullet.tscn"),
        }
      )
      .WithParent(GameInstance.Instance);
  }
}
