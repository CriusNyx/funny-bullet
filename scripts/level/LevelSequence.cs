using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class LevelSequence : LevelNode
{
  private IEnumerator<LevelNode>? children;

  public override void Start()
  {
    children = this.GetChildrenOfType<LevelNode>().GetEnumerator();
    MoveNextNode();
  }

  void MoveNextNode()
  {
    if (children?.MoveNext() ?? false)
    {
      children.Current.Start();
    }
    else
    {
      children = null;
      Finish();
    }
  }

  public override void Update(double deltaTime)
  {
    if (children != null)
    {
      children.Current.Update(deltaTime);
      if (children.Current.IsDone)
      {
        MoveNextNode();
      }
    }
  }
}
