using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class BulletSpawner : Node3D
{
  private double time;
  public IEnumerator<BulletSpawn> spawns = null!;
  public SpawnParameters spawnParameters;

  public override void _Ready()
  {
    base._Ready();
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    while (NextBullet() is BulletSpawn nextBullet)
    {
      Bullet.Spawn(nextBullet.WithParameters(spawnParameters), Position, Quaternion);
    }

    if (spawns == null)
    {
      QueueFree();
    }

    time += delta;
  }

  private bool MoveNext()
  {
    if (spawns == null)
    {
      return false;
    }
    if (!spawns.MoveNext())
    {
      spawns = null!;
      return false;
    }
    return true;
  }

  private BulletSpawn? NextBullet()
  {
    if (spawns?.Current.spawnTime <= time)
    {
      var output = spawns.Current;
      MoveNext();
      return output;
    }
    return null;
  }

  public BulletSpawner WithSpawn(IEnumerable<BulletSpawn> spawns, SpawnParameters spawnParameters)
  {
    this.spawns = spawns.OrderBy(x => x.spawnTime).GetEnumerator();
    this.spawns.MoveNext();
    this.spawnParameters = spawnParameters;
    return this;
  }

  public static BulletSpawner Create(
    IEnumerable<BulletSpawn> spawn,
    SpawnParameters spawnParameters,
    Node3D parent,
    Vector3? position = null,
    Node3D? target = null
  )
  {
    var pos = position ?? parent.Position;
    var rotation = Quaternion.Identity;
    if (target != null)
    {
      var targetDirection = (target.Position - pos).Normalized();
      var theta = Mathf.Atan2(targetDirection.Y, targetDirection.X);
      rotation = Quaternion.FromEuler(new Vector3(0, 0, theta));
    }

    return new BulletSpawner()
      .WithParent(parent)
      .WithSpawn(spawn, spawnParameters)
      .WithTransform(position ?? parent.Position, rotation);
  }
}
