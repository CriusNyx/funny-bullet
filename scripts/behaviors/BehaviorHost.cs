using Godot;

[GlobalClass]
public partial class BehaviorHost : Node3D
{
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
    this.OnBehaviorEvent(e, sender);
    this.GetChildrenOfType<Behavior>().Foreach(x => x.OnBehaviorEvent(e, sender));
  }

  public virtual void OnBehaviorEvent(BehaviorEvent e, Behavior sender) { }
}
