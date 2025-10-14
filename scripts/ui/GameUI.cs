using System;
using System.Linq;
using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class GameUI : Control
{
  public const string DIALOG_PANEL_NAME = "DialogPanel";
  public const string DIALOG_LABEL_NAME = "DialogController";
  public const string LEFT_PORTRAIT_NAME = "LeftPortrait";
  public const string RIGHT_PORTRAIT_NAME = "RightPortrait";
  public static GameUI Instance => Game.Instance.NotNull().gameUI;

  public Control DialogPanel { get; private set; } = null!;
  public TextureRect LeftPortrait { get; private set; } = null!;
  public TextureRect RightPortrait { get; private set; } = null!;
  public DialogController DialogController { get; private set; } = null!;

  public override void _EnterTree()
  {
    DialogPanel = FindChild(DIALOG_PANEL_NAME).As<Control>().NotNull();
    DialogPanel.Visible = false;
    DialogController = FindChild(DIALOG_LABEL_NAME).As<DialogController>().NotNull();
    LeftPortrait = FindChild(LEFT_PORTRAIT_NAME).As<TextureRect>().NotNull();
    RightPortrait = FindChild(RIGHT_PORTRAIT_NAME).As<TextureRect>().NotNull();
  }

  public async Task ShowDialog(Dialog dialog)
  {
    DialogPanel.Visible = true;
    foreach (var content in dialog.GetContent())
    {
      await DialogController.PlayDialogContent(content);
    }
    DialogPanel.Visible = false;
  }
}
