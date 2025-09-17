using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class BulletSpawner : Node3D
{
  private double time;
  public IEnumerator<BulletSpawn> spawns;
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
      Bullet.Spawn(nextBullet.WithParameters(spawnParameters), spawnParameters.prefab);
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
      spawns = null;
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
    Vector3? position = null
  )
  {
    return new BulletSpawner()
      .WithParent(parent)
      .WithSpawn(spawn, spawnParameters)
      .WithTransform(position ?? parent.Position);
  }
}
