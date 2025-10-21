using System.Linq;
using Godot;

[GlobalClass]
public partial class Wave : LevelNode
{
  [Export]
  int trackerCount { get; set; }

  public override void Start()
  {
    base.Start();
    foreach (var animationPlayer in this.GetChildrenOfType<AnimationPlayer>())
    {
      animationPlayer.Play(GameStats.START_ANIMATION_NAME);
    }
  }

  public override void Update(double deltaTime)
  {
    var animationCount = this.GetChildrenOfType<AnimationPlayer>().Count(x => x.IsPlaying());
    if (trackerCount == 0 && animationCount == 0)
    {
      GD.Print("Finished");
      Finish();
    }
  }

  public void TrackOne()
  {
    trackerCount++;
  }

  public void UntrackOne()
  {
    trackerCount--;
  }
}
