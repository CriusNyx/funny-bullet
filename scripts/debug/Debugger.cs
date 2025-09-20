using Godot;

[GlobalClass]
public partial class Debugger : Node2D
{
  Label label;

  public static Debugger Instance => GameInstance.Instance.Debugger;

  public string LevelInterpreterString { get; private set; }

  public override void _Ready()
  {
    label = new Label().WithParent(this);
    label.Text = "Foo";
  }

  public override void _Process(double delta)
  {
    label.Text = string.Join("\n\n", LevelInterpreterString);
  }

  public static void SetLevelInterpreterString(string value)
  {
    Instance.LevelInterpreterString = value;
  }
}
