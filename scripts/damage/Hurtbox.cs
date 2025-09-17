using Godot;
using static Godot.GD;

[GlobalClass]
public partial class Hurtbox : CharacterBody3D
{
  [Export]
  public DamageFiler canBeHurtBy;
  private CircleQueue<Hitbox> currentCollisions = new CircleQueue<Hitbox>(16);

  public override void _Ready()
  {
    base._Ready();
  }

  private bool CanBeHurtBy(Hitbox other)
  {
    return (int)(canBeHurtBy & other.damageSource) != 0;
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    if (MoveAndCollide(Vector3.Zero, true) is KinematicCollision3D collision)
    {
      CheckCollisionForDamage(collision);
    }
  }

  private void CheckCollisionForDamage(KinematicCollision3D collision)
  {
    if (collision.GetCollider().As<Node>()?.GetParentOfType<Hitbox>() is Hitbox hitbox)
    {
      HandleProspectiveCollision(hitbox);
    }
  }

  private void HandleProspectiveCollision(Hitbox hitbox)
  {
    if (CanBeHurtBy(hitbox))
    {
      if (!currentCollisions.Contains(hitbox))
      {
        currentCollisions.Enqueue(hitbox);
        HandleHurt(hitbox);
      }
    }
  }

  private void HandleHurt(Hitbox hitbox)
  {
    hitbox.OnHit(this);
    OnHurt(hitbox);
  }

  public void OnHurt(Hitbox hitbox)
  {
    foreach (var parent in this.GetParentsOfType<IHandleHurtboxEvents>())
    {
      parent.OnHurt(hitbox, this);
    }
  }
}

interface IHandleHurtboxEvents
{
  void OnHurt(Hitbox hitbox, Hurtbox hurtbox);
}
