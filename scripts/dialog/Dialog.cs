using System.Collections.Generic;
using Godot;

public enum PortraitSide
{
  Left,
  Right,
  None,
}

[GlobalClass]
public partial class Dialog : LevelNode
{
  [Export]
  public Texture2D? texture;

  [Export]
  public bool flipped;

  [Export]
  public PortraitSide side;

  public IEnumerable<DialogContent> GetContent()
  {
    return this.GetChildrenOfType<DialogContent>();
  }

  public override void Start()
  {
    GameUI.Instance.SetPortrait(side, texture, flipped);
    _ = GameUI.Instance.ShowDialog(this).Then(Finish);
  }
}
