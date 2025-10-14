using Godot;

/// <summary>
/// A behavior to attach to a behavior host. A behavior host will update each behavior in it's children each frame.
/// Children will be updated in the same order as their order in the tree.
/// </summary>
public interface Behavior
{
  void HostUpdate(Actor host, double deltaTime) { }
  void HostPhysicsUpdate(Actor host, double deltaTime) { }
  void OnBehaviorEvent(BehaviorEvent e, Behavior sender) { }
}

public static class BehaviorExtensions
{
  public static Actor? GetActor<T>(this T behavior)
    where T : Node, Behavior
  {
    return behavior.GetParentOfType<Actor>().TouchIfNull(() => behavior.WarnNoHost());
  }

  public static void WarnNoHost(this Behavior behavior)
  {
    GD.PushWarning(
      $"Animated behavior {behavior.As<Node>()?.Name ?? behavior.GetType().Name} tried to find a host, but no host was present in the graph"
    );
  }

  public static void BroadcastEvent<T>(this T behavior, BehaviorEvent e)
    where T : Node, Behavior
  {
    behavior.GetActor()?.BroadcastEvent(e, behavior);
  }
}
