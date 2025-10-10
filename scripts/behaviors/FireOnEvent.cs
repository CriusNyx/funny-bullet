using Godot;

/// <summary>
/// Transforms events of the specified type to fire events.cc
/// </summary>
[GlobalClass]
public partial class FireOnEvent : Node, Behavior
{
  [Export]
  public BehaviorEventType eventType;

  [Export]
  public string? firePattern;

  [Export]
  public FireParameters? fireParameters = null;

  public void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    // Prevent recursively rebroadcasting events.
    if (sender == this)
    {
      return;
    }
    if (e.Type == eventType)
    {
      this.BroadcastEvent(new FireEvent { fireParameters = fireParameters });
    }
  }
}
