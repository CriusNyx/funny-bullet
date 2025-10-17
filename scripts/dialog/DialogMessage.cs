using Godot;

public interface IDialogMessage
{
  public string Message { get; }
}

[GlobalClass]
public partial class DialogMessage : Node, DialogContent, IDialogMessage
{
  [Export(PropertyHint.MultilineText)]
  public string Message { get; private set; } = "";
}
