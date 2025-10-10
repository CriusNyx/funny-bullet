using System;
using Godot;

public partial class OnDeath : Node, Behavior
{
  public bool handled = false;
  Action<Character?> handler;

  public OnDeath(Action<Character?> handler)
  {
    this.handler = handler;
  }

  public void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    if (e is DeathEvent de && this.GetActor() is Character character)
    {
      HandleDeath(character);
    }
  }

  private void HandleDeath(Character? character)
  {
    if (!handled)
    {
      handler?.Invoke(character);
      handled = true;
      QueueFree();
    }
  }

  public override void _ExitTree()
  {
    HandleDeath(this.GetParentOfType<Character>());
  }
}

public static class OnDeathExtensions
{
  public static T WithOnDeath<T>(this T node, Action<Character?> action)
    where T : Node
  {
    node.AddChild(new OnDeath(action));
    return node;
  }
}
