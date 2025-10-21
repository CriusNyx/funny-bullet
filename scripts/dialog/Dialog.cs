using FunnyBullet.Dialog;
using Godot;

public partial class DialogPlayerProxy : GodotObject
{
  DialogPlayer player;

  public DialogPlayerProxy(DialogPlayer player)
  {
    this.player = player;
  }

  public void jump(string label)
  {
    player.Jump(label);
  }
}

[GlobalClass]
public partial class Dialog : LevelNode, DialogPlayerImplementation
{
  DialogPlayer player;

  [Export(PropertyHint.File, "*.md")]
  public string dialogFilePath = null!;

  public override void _Ready()
  {
    player = new DialogPlayer(this);
    base._Ready();
  }

  public void OnMeta(DialogPlayer player, DialogFile.DialogMeta meta)
  {
    GameUI.Instance.DialogController.SetVars(meta.variables);
  }

  public void StartActor(DialogPlayer player, StartActorArgs args)
  {
    GameUI.Instance.ShowDialogPanel();
  }

  public void StopActor(DialogPlayer player, StopActorArgs args)
  {
    GameUI.Instance.HideDialogPanel();
  }

  public void PlayMessage(DialogPlayer player, PlayMessageArgs args)
  {
    GameUI.Instance.DialogController.PlayMessage(args.message.message);
  }

  public void InterpretCode(DialogPlayer player, InterpretCodeArgs args)
  {
    Expression expression = new();
    expression.Parse(args.codeBlock.code, ["node", "player"]);
    expression.Execute([this, new DialogPlayerProxy(player)]);
    player.OnCodeFinished(args.codeBlock);
  }

  public void OnDialogFinished(DialogPlayer player)
  {
    GameUI.Instance.DialogController.OnMessageFinished -= HandleMessageFinished;
    Finish();
  }

  public override void Start()
  {
    GameUI.Instance.DialogController.OnMessageFinished += HandleMessageFinished;
    var file = FileAccess.Open(this.dialogFilePath, FileAccess.ModeFlags.Read);
    var content = file.GetAsText();
    var dialogFile = DialogFile.Parse(content);
    player.SetDialogFile(dialogFile);
  }

  public void HandleMessageFinished()
  {
    player.OnMessageFinished();
  }
}
