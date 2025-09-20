using System;
using Godot;

public abstract partial class Character : Node3D
{
  public Health? health;

  public override void _Ready()
  {
    base._Ready();
    health = this.GetChildOfType<Health>();
  }

  public void Kill()
  {
    OnKilled(this);
    foreach (var child in this.GetChildrenOfType<HandleKilled>())
    {
      child.OnKilled(this);
    }
  }

  protected virtual void OnKilled(Character character)
  {
    QueueFree();
  }

  public Character OnDeath(Action<Character> handler)
  {
    AddChild(new OnDeath(handler));
    return this;
  }
}

interface HandleKilled
{
  void OnKilled(Character character);
}
