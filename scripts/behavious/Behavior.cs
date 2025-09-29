using Godot;

[GlobalClass]
public abstract partial class Behavior : Node
{
  public BehaviorHost? Host => this.GetParentOfType<BehaviorHost>().TouchIfNull(WarnNoHost);

  public void WarnNoHost()
  {
    GD.PushWarning(
      $"Animated behavior {Name} tried to find a host, but no host was present in the graph"
    );
  }
}
