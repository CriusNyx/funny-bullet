using Godot;

[GlobalClass]
public partial class Bullet : Node3D, IHandleHitboxEvents
{
  double timeAlive;
  public BulletSpawn spawn;
  Vector3 fireDirection;

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

    var direction = fireDirection.Rotated(Vector3.Back, Mathf.DegToRad(spawn.angle));
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

  public static Bullet Spawn(BulletSpawn spawn, Vector3 position, Quaternion? rotation = null)
  {
    var instance = spawn.prefab?.Instantiate<Bullet>() ?? new Bullet();
    return instance
      .WithParent(Game.Instance)
      .WithSpawn(spawn)
      .WithTransform(position, rotation)
      .WithName("Bullet")
      .Touch(x => x.fireDirection = (rotation ?? Quaternion.Identity) * Vector3.Right);
  }

  public void OnHit(Hitbox hitbox, Hurtbox hurtbox)
  {
    if (destroyOnHit)
    {
      QueueFree();
    }
  }
}
