using Godot;

/// <summary>
/// A behavior that runs an animation timeline in the children.
/// Runs the start animation on the timeline.
/// </summary>
[GlobalClass]
public partial class AnimatedBehavior : Node, Behavior
{
  public override void _Ready()
  {
    foreach (var child in this.GetChildrenOfType<AnimationPlayer>())
    {
      child.Play(GameStats.START_ANIMATION_NAME);
    }
  }
}
