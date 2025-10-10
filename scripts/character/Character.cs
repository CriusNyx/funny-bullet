using System;

public abstract partial class Character : Actor
{
  public virtual bool DestroyOnLeaveBounds => true;

  public override void _Ready()
  {
    base._Ready();
  }

  public override void _Process(double deltaTime)
  {
    base._Process(deltaTime);
    if (!SafeSpace.Contains(Position.To2()))
    {
      OnLeaveSafeBounds();
      if (DestroyOnLeaveBounds)
      {
        Kill();
      }
    }
  }

  public override void _PhysicsProcess(double deltaTime)
  {
    base._PhysicsProcess(deltaTime);
  }

  public void Kill()
  {
    OnKilled(this);
  }

  protected virtual void OnKilled(Character character)
  {
    QueueFree();
  }

  public Character OnDeath(Action<Character?> handler)
  {
    AddChild(new OnDeath(handler));
    return this;
  }

  public virtual void OnLeaveSafeBounds() { }
}
