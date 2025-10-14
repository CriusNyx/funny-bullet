using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class Dialog : LevelNode
{
  public IEnumerable<DialogContent> GetContent()
  {
    return this.GetChildrenOfType<DialogContent>();
  }

  public override void Start()
  {
    _ = GameUI.Instance.ShowDialog(this);
  }
}
