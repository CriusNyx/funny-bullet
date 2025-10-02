using Godot;

[GlobalClass]
public partial class AnimatedBehavior : Behavior
{
  public override void _Ready()
  {
    foreach (var child in this.GetChildrenOfType<AnimationPlayer>())
    {
      child.Play(GameStats.START_ANIMATION_NAME);
    }
  }
}
