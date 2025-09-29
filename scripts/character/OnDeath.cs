using System;
using Godot;

public partial class OnDeath : Node, HandleKilled
{
  public bool handled = false;
  Action<Character?> handler;

  public OnDeath(Action<Character?> handler)
  {
    this.handler = handler;
  }

  public void OnKilled(Character? character)
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
    OnKilled(this.GetParentOfType<Character>());
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
