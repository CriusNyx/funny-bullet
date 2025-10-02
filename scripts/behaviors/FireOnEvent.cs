using Godot;

[GlobalClass]
public partial class FireOnEvent : Behavior
{
  [Export]
  public BehaviorEventType eventType;

  [Export]
  public string? firePattern;

  [Export]
  public FireParameters? fireParameters = null;

  public override void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    // Prevent recursively rebroadcasting events.
    if (sender == this)
    {
      return;
    }
    if (e.Type == eventType)
    {
      BroadcastEvent(new FireEvent { fireParameters = fireParameters });
    }
  }
}
