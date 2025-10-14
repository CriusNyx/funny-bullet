using Godot;

[GlobalClass]
public partial class DialogMessage : DialogContent
{
  [Export]
  public int characterRate = 10;

  [Export(PropertyHint.MultilineText)]
  public string Message = "";
}
