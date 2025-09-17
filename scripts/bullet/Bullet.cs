using Godot;
using static Godot.GD;

[GlobalClass]
public partial class Bullet : Node3D, IHandleHitboxEvents
{
  double timeAlive;
  public BulletSpawn spawn;

  [Export(PropertyHint.Flags)]
  public DamageFiler damageSource;

  [Export]
  public bool destroyOnHit;

  public override void _Ready()
  {
    base._Ready();
    Position = spawn.position.To3();
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    var direction = Vector3.Right.Rotated(Vector3.Back, Mathf.DegToRad(spawn.angle));
    var offset = direction * spawn.speed * (float)delta;
    Position += offset;

    timeAlive += delta;

    if (timeAlive > spawn.lifetime)
    {
      QueueFree();
    }
  }

  private Bullet WithSpawn(BulletSpawn spawn)
  {
    this.spawn = spawn.Clone(angle: spawn.angle);
    return this;
  }

  public static Bullet Spawn(BulletSpawn spawn, PackedScene prefab = null)
  {
    var instance = prefab?.Instantiate<Bullet>() ?? new Bullet();
    return instance.WithParent(GameInstance.Instance).WithSpawn(spawn).WithName("Bullet");
  }

  public void OnHit(Hitbox hitbox, Hurtbox hurtbox)
  {
    if (destroyOnHit)
    {
      QueueFree();
    }
  }
}
