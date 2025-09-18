using System;
using Godot;

[GlobalClass]
public partial class Hitbox : Node3D
{
  [Export]
  public DamageFiler damageSource;

  public void OnHit(Hurtbox other)
  {
    foreach (var parent in this.GetParentsOfType<IHandleHitboxEvents>())
    {
      parent.OnHit(this, other);
    }
  }
}

interface IHandleHitboxEvents
{
  void OnHit(Hitbox hitbox, Hurtbox hurtbox);
}
