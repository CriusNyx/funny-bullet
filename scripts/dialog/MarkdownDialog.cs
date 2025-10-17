using System.Threading.Tasks;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class MarkdownDialog : LevelNode
{
  [Export]
  public Dictionary<string, Texture2D>? actorPortraits;

  [Export(PropertyHint.FilePath, "*.md")]
  string filePath = null!;

  public MarkdownDialog() { }

  public override void Start()
  {
    if (filePath.WarnNull() is string str)
    {
      var fileText = FileAccess.GetFileAsString(filePath);
      var dialogInfo = DialogInfo.Parse(fileText);

      _ = PlayDialog(dialogInfo).Then(() => Finish());
    }
  }

  public async Task PlayDialog(DialogInfo dialogInfo)
  {
    foreach (var dialog in dialogInfo.dialogBoxes)
    {
      var actorInfo = dialogInfo.parameters?.actors?.Safe(dialog.actorName);
      var portrait = actorPortraits?.Safe(dialog.actorName);
      GameUI.Instance.SetPortrait(
        actorInfo?.side ?? PortraitSide.Right,
        portrait,
        actorInfo?.flipped ?? false
      );

      await GameUI.Instance.ShowDialogContent(dialog.lines, dialogInfo.parameters?.vars);
    }
  }
}
