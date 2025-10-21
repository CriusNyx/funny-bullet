using System;
using System.Collections.Generic;
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

  public void SetPortrait(PortraitSide side, Texture2D? texture, bool flipped)
  {
    LeftPortrait.Visible = false;
    RightPortrait.Visible = false;

    void SetTexture(TextureRect rect)
    {
      rect.Texture = texture;
      rect.Visible = true;
      rect.FlipH = flipped;
    }

    if (texture != null)
    {
      if (side == PortraitSide.Left)
      {
        SetTexture(LeftPortrait);
      }
      if (side == PortraitSide.Right)
      {
        SetTexture(RightPortrait);
      }
    }
  }

  public void ShowDialogPanel()
  {
    DialogPanel.Visible = true;
  }

  public void HideDialogPanel()
  {
    DialogPanel.Visible = false;
  }

  public async Task ShowDialog(Dialog dialog, Dictionary<string, string>? vars = null)
  {
    throw new NotImplementedException();
    // await ShowDialogContent(dialog.GetContent(), vars);
  }
}
