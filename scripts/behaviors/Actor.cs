using Godot;

/// <summary>
/// Base class that hosts behaviors for children.
/// </summary>
[GlobalClass]
public partial class Actor : Node3D
{
  public Health? Health => this.GetBehavior<Health>();

  public override void _Process(double deltaTime)
  {
    this.GetChildrenOfType<Behavior>().Foreach(child => child.HostUpdate(this, deltaTime));
  }

  public override void _PhysicsProcess(double deltaTime)
  {
    this.GetChildrenOfType<Behavior>().Foreach(child => child.HostPhysicsUpdate(this, deltaTime));
  }

  public void BroadcastEvent(BehaviorEvent e, Behavior sender)
  {
    OnBehaviorEvent(e, sender);
    this.GetChildrenOfType<Behavior>().Foreach(x => x.OnBehaviorEvent(e, sender));
  }

  public virtual void OnBehaviorEvent(BehaviorEvent e, Behavior sender) { }

  public BehaviorT? GetBehavior<BehaviorT>()
  {
    return this.GetChildOfType<BehaviorT>();
  }
}
